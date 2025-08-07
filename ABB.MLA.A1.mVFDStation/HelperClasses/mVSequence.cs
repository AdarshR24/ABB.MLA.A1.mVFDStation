using System;
using System.ComponentModel;
using System.Data;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{
    public class mVSequence : INotifyPropertyChanged
    {
        private CancellationTokenSource mvSeqTokenSource;
        public event PropertyChangedEventHandler PropertyChanged;
        public string R = "0", Y = "0", B = "0", N = "0";
        private string mVSeqeunce = "CheckCommCmd";
        private DateTime Timeout, etime;
        private int tryonce = 0;

        private readonly mVSource mVTest;
        private readonly ModbusClass ModbusTCP;
        private readonly SerialPort Multimter;

        public string MultimeterData = "";
        public string Resp = "";

        public float TestCurrent { get; set; } = 1.5f;
        public string TbSerialNo { get; set; } = "";

        // Bindable Properties
        private float _rmv, _ymv, _bmv, _nmv;
        public float Rmv { get => _rmv; private set { _rmv = value; OnPropertyChanged(nameof(Rmv)); } }
        public float Ymv { get => _ymv; private set { _ymv = value; OnPropertyChanged(nameof(Ymv)); } }
        public float Bmv { get => _bmv; private set { _bmv = value; OnPropertyChanged(nameof(Bmv)); } }
        public float Nmv { get => _nmv; private set { _nmv = value; OnPropertyChanged(nameof(Nmv)); } }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public mVSequence(mVSource mV, ModbusClass modbus,string COM)
        {
            mVTest = mV;
            ModbusTCP = modbus;
            Multimter = new SerialPort(COM);
            Multimter.BaudRate = 115200;
            Multimter.Open();
            Multimter.DataReceived += Multimter_DataReceived;
        }
        private void Multimter_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (mVSeqeunce != "")
            {
                try
                {
                    int l = Multimter.BytesToRead;
                    byte[] buf = new byte[Multimter.BytesToRead];
                    Multimter.Read(buf, 0, buf.Length);
                    string k = System.Text.ASCIIEncoding.ASCII.GetString(buf).Trim('\r', '\n');
                    string[] KL = k.Split(',');
                    float val = float.Parse(KL[0]);
                    if (mVSeqeunce == "ReadmilliVolt")
                    {
                        MultimeterData = KL[0];
                    }
                    Multimter.DiscardInBuffer();
                }
                catch (Exception ex)
                {
                    //WriteErrortoPLC(-2);
                }
            }
            Thread.Sleep(100);
        }

        public void Start()
        {
            if (mvSeqTokenSource != null && !mvSeqTokenSource.IsCancellationRequested)
                return;
            mVSeqeunce = "CheckCommCmd";
            mvSeqTokenSource = new CancellationTokenSource();
            Task.Run(() => RunMVSequenceLoop(mvSeqTokenSource.Token));
        }

        public void Stop() => mvSeqTokenSource?.Cancel();

        private async Task RunMVSequenceLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !string.IsNullOrEmpty(mVSeqeunce))
            {
                try
                {
                    #region Mv Sequence
                    switch (mVSeqeunce)
                    {
                        case "CheckCommCmd":
                            if (mVTest.tryOpen() & TryOpen())
                            {
                                Console.WriteLine("Mul Conencection-->" + Multimter.IsOpen);
                                mVTest.mVWrite(200, "ADR 06");
                                mVSeqeunce = "ReadComms";
                                Timeout = DateTime.Now + TimeSpan.FromSeconds(5);
                                Console.WriteLine("ADR06 Ready Comand sent to soruce");
                            }
                            else
                            {
                                //  WriteErrortoPLC(-1);
                                MessageBox.Show($"Connection Error source--{mVTest.isopen()} multinmryer--{Multimter.IsOpen} ");
                                Console.WriteLine("Port Opening Failed");
                                Console.WriteLine("OK FB Not recieved");
                                //WriteErrortoPLC(-1);
                            }
                            break;
                        case "ReadComms":
                            Resp = mVTest.Read();
                            Console.WriteLine($"Waiting for FB--{Resp}");
                            if (Resp == "OK")
                            {
                                mVTest.mVWrite(100, "OUT 0");
                                mVSeqeunce = "SetVoltage";
                                Console.WriteLine("Volateg Settting");

                            }
                            if (DateTime.Now > Timeout)
                            {
                                Console.WriteLine("OK FB Not recieved");
                                //WriteErrortoPLC(-1);
                            }
                            break;
                        case "SetVoltage":
                            mVTest.mVWrite(50, "PV 10.0"); // Voltage Settings
                            Thread.Sleep(150);
                            mVTest.mVWrite(50, $"PC 30"); /// current settings for mV Soruce
                            Thread.Sleep(150);
                            mVTest.mVWrite(50, "OUT 1");
                            Thread.Sleep(2500);
                            Timeout = DateTime.Now + TimeSpan.FromSeconds(5);
                            mVTest.mVWrite(100, "STT?");
                            Thread.Sleep(100);
                            mVSeqeunce = "ReadVoltage";
                            break;
                        case "ReadVoltage":
                            Resp = mVTest.Read();
                            Console.WriteLine("Response-->" + Resp);
                            if (Resp == "Waiting")
                            {

                            }
                            else if (Resp == "Err")
                            {
                                //WriteErrortoPLC(-1);
                            }
                            else if (Resp.StartsWith("MV"))
                            {
                                if (IsMCWithinRange(Resp, 30, (float)15))
                                {
                                    mVSeqeunce = "TurnOPON";
                                }
                                else
                                {
                                    MessageBox.Show("No Current Flow");
                                }

                            }
                            if (DateTime.Now > Timeout)
                            {
                                //WriteErrortoPLC(-2);
                            }
                            break;
                        case "TurnOPON":
                            ModbusTCP.WriteSingleCoil("402", false);
                            mVSeqeunce = "ReadCurrent";
                            Thread.Sleep(200);
                            break;

                        case "ReadCurrent":
                            mVSeqeunce = "CheckCurrentDraw";
                            break;

                        case "CheckCurrentDraw":
                            Console.WriteLine("Check Current Drawe");
                            Multimter.Write("read? \r\n");
                            etime = DateTime.Now + TimeSpan.FromSeconds(2);
                            Timeout = DateTime.Now + TimeSpan.FromSeconds(10);
                            mVSeqeunce = "ReadmilliVolt"; //MEASure:VOLTage:AC? 
                            displayUpdate(ModbusTCP.PLCWord[41], 0);
                            break;
                        case "ReadmilliVolt":

                            if (MultimeterData.Length > 0)
                            {
                                float k = float.Parse(MultimeterData) * 1000;
                                switch (ModbusTCP.PLCWord[41])
                                {
                                    case 1:
                                        R = k.ToString();
                                        PoleValidation();
                                        break;
                                    case 2:
                                        Y = k.ToString();
                                        PoleValidation();
                                        break;
                                    case 3:
                                        B = k.ToString();
                                        PoleValidation();
                                        break;
                                    case 4:
                                        N = k.ToString();
                                        PoleValidation();
                                        break;
                                }
                                ModbusTCP.WriteSingleCoil("405", true);
                                mVSeqeunce = "UpdateScreen";
                                MultimeterData = "";
                            }
                            if (tryonce == 0 && DateTime.Now > Timeout - TimeSpan.FromSeconds(3))
                            {
                                tryonce = 1;
                                mVSeqeunce = "CheckCurrentDraw";
                            }
                            else
                            {
                                Console.WriteLine($"mm>" + MultimeterData + "Timeout-" + DateTime.Now + "==" + Timeout);
                            }

                            if (DateTime.Now > Timeout)
                            {
                                Console.WriteLine($"mmtIMEOUT" + Timeout);

                                tryonce = 0;
                                //WriteErrortoPLC(-2);
                            }
                            break;
                        case "UpdateScreen":
                            tryonce = 0;
                            if (ModbusTCP.PLCbit[402])
                            {
                                mVSeqeunce = "TurnOPON";
                            }
                            else if (ModbusTCP.PLCbit[416])
                            {
                                ModbusTCP.WriteSingleCoil("415", false);
                                Thread.Sleep(200);
                                mVTest.closeconnection();
                                Multimter.Close();
                                //App.ModbusTCP.WriteSingleCoil(416, false);
                                mVSeqeunce = "";
                            }

                            break;
                        case "CommunicationErrorUpdate":

                            break;
                        case "TestCompleted":

                            break;

                    }
                    #endregion        }

                    await Task.Delay(50); // Tick simulation
                }
                catch (Exception ex)
                {
                    WriteErrorToPLC("Exception: " + ex.Message);
                    mVSeqeunce = "";
                }
            }
        }

        // Helpers
        private bool TryOpen() => Multimter != null && Multimter.IsOpen;

        public static bool IsMCWithinRange(string input, double setValue, double percentage)
        {
            try
            {
                var match = Regex.Match(input, @"MC\((\d+(\.\d+)?)\)");
                if (match.Success)
                {
                    // Parse the MC value
                    double mcValue = double.Parse(match.Groups[1].Value);

                    // Calculate the acceptable range
                    double lowerBound = setValue - (setValue * percentage / 100);
                    double upperBound = setValue + (setValue * percentage / 100);

                    // Check if MC value is within the range
                    return mcValue >= lowerBound && mcValue <= upperBound;
                }
                else
                {
                    Console.WriteLine("MC value not found in the input string.");
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void WriteErrorToPLC(string code)
        {
            Console.WriteLine($"PLC Error: {code}");
            // Example: ModbusTCP.WriteSingleRegister(500, int.Parse(code));
        }
        public void displayUpdate(int Pos, float mv)
        {
            switch (Pos)
            {
                //case 1:
                //    tb_P1.Text = mv.ToString();
                //    break;
                //case 2:
                //    tb_P2.Text = mv.ToString();
                //    break;
                //case 3:
                //    tb_P3.Text = mv.ToString();
                //    break;
                //case 4:
                //    tb_P4.Text = mv.ToString();
                //    break;
            }
        }

        private void PoleValidation()
        {
            ModbusTCP.WriteSingleCoil("403", true); //PASS BIT 
        }


        private void DisplayUpdate()
        {
            // Optional UI update logic
        }
    }
}
