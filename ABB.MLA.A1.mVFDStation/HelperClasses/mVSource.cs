using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{
    public class mVSource
    {
        SerialPort SourceSerial = new SerialPort();
        SerialPort MultiMterSerial = new SerialPort();
        private float _mVVoltage;
        public DateTime SendTime { get; set; }
        public DateTime Recievetime { get; set; }
        public bool isbusy = false;
        public float mvVoltage
        {
            get { return _mVVoltage; }
            set { _mVVoltage = value; }
        }

        public mVSource(string COMPort)
        {
            try
            {
                SourceSerial.PortName = COMPort;
                SourceSerial.BaudRate = 9600;
                SourceSerial.Parity = Parity.None;
                SourceSerial.DataBits = 8;
                SourceSerial.StopBits = StopBits.One;
                SourceSerial.Handshake = Handshake.XOnXOff;
                SourceSerial.Encoding = Encoding.ASCII;
                if (ConnectionOpen())
                {
                    //App.ModbusTCP.WriteSingleCoil(438, false);
                }
                else
                {
                    //App.ModbusTCP.WriteSingleCoil(438, true);

                }
            }
            catch
            {
                //App.ModbusTCP.WriteSingleCoil(438, true);
            }
        }

        public bool ConnectionOpen()
        {

            if (SourceSerial.IsOpen)
            {
                return true;
            }
            else
            {
                SourceSerial.Open();
                return true;
            }
        }

        public bool mVWrite(int RTime, string cmd)
        {
            try
            {
                SourceSerial.DiscardInBuffer();
                Thread.Sleep(100);
                SourceSerial.Write(cmd + "\r\n");
                Recievetime = DateTime.Now + TimeSpan.FromMilliseconds(RTime);
                isbusy = true;
                return true;
            }
            catch
            {
                isbusy = false;
                return false;

            }
        }

        public bool isopen()
        {
            return SourceSerial.IsOpen;
        }
        public bool tryOpen()
        {
            try
            {
                if (SourceSerial.IsOpen)
                {
                    return true;
                }
                else
                {
                    SourceSerial.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void closeconnection()
        {
            mVWrite(0, "OUT 0");

            SourceSerial.Close();
        }
        public string Read()
        {
            if (DateTime.Now >= Recievetime)
            {
                try
                {
                    int l = SourceSerial.BytesToRead;
                    byte[] buf = new byte[SourceSerial.BytesToRead];
                    SourceSerial.Read(buf, 0, buf.Length);
                    string k = System.Text.ASCIIEncoding.ASCII.GetString(buf).Trim('\r', '\n');

                    isbusy = false;
                    return k;
                }
                catch
                {
                    return "Err";
                }
            }
            else
            {
                isbusy = true;
                return "Waiting";
            }
        }

    }

}
