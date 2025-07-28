using FluentModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Collections.Concurrent;
using Microsoft.Win32;
using System.Security.Policy;
using System.CodeDom;
using System.Net;
using System.Buffers.Binary;

using System.Reflection;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{
    public class ModbusClass : INotifyPropertyChanged
    {

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private ModbusTcpClient _modbusClient;
        private readonly string _plcIpAddress;
        private readonly int _plcPort;
        public event EventHandler DataRefreshed,PLCConnected;

        
        public bool isInitalised { get; set; }

        private ConcurrentQueue<PLCEnum> OperationQueue = new ConcurrentQueue<PLCEnum>();
       
        private bool _ishealthy;

        private string Status;

        public bool ConnectionError { get; set; }

        public string ip { get; set; }
        public int CycleTime { get; set; }
        public string status
        {
            get { return Status; }
            set
            {
                if (Status != value)
                {
                    Status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }


        public bool IsHealthy
        {
            get { return _ishealthy; }
            set
            {
                if (_ishealthy != value)
                {
                    _ishealthy = value;
                    OnPropertyChanged(nameof(_ishealthy));
                }
            }
        }

        private static ModbusClass _modbusInstance; // Singleton instance
        private static readonly object _lock = new object(); // Lock for thread safety

        // Private constructor to prevent direct instantiation.

        // Public static method to get the singleton instance.
        public static ModbusClass GetInstance(string ip, int port)
        {
            if (_modbusInstance == null)
            {

            }
            return _modbusInstance;
        }

        private bool _connectionEstablished;

        public bool ConnectionEstablished
        {
            get { return _connectionEstablished; }
            set {
                if (_connectionEstablished != value)
                {
                    _connectionEstablished = value;
                    if (ConnectionEstablished)
                    {
                        PLCConnected?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }


        public void PingTest()
        {
            IPAddress ipAddress = IPAddress.Parse(_plcIpAddress);

        }

        public string OverAllErrors { get; set; }

        public void Initialize(string ip, int port)
        {
            this.ip = ip;
            status = "Ready";
            if (isInitalised)
            {
                status = "ModbusClass is already initialized!";
                if (!_modbusClient.IsConnected)
                {
                    try
                    {
                        _modbusClient.Connect(ip, ModbusEndianness.BigEndian);
                    }
                    catch
                    {
                        DataRefreshed?.Invoke(this, EventArgs.Empty);
                        isInitalised = false;
                        status = "Modbus connection failed.";
                    }
                }
                return;
            }
            try
            {
                _plcbit = new bool[15000];
                Array.Clear(PLCbit, 0, PLCbit.Length); // sets all elements to false
                _modbusClient = new ModbusTcpClient();
                _modbusClient.Connect(ip, ModbusEndianness.BigEndian);
                isInitalised = _modbusClient.IsConnected;
              

                status = "Communication Successful";
                _ishealthy = true;
                _cancellationTokenSource = new CancellationTokenSource();
                _ = Task.Run(() => FetchDataContinuously(_cancellationTokenSource.Token));

                if (isInitalised)
                {
                    status = "Modbus connection successful!";
                }
                else
                {
                    status = "Modbus connection failed.";
                }
            }
            catch (Exception ex)
            {
                ConnectionError = true;
                //ErrorHandler.Instance.OverallStationErrors.PLCConnectionError = true;
                DataRefreshed?.Invoke(this, EventArgs.Empty);
                status = $"Modbus initialization error: {ex.Message}";
                MessageBox.Show(status);
            }
        }

      

        public short[] PLCWord = new short[900];

        public event PropertyChangedEventHandler PropertyChanged;

        private bool[] _plcbit;

        public bool[] PLCbit
        {
            get { return _plcbit; }
            set
            {

                _plcbit = value;
                OnPropertyChanged(nameof(PLCbit));
            }
        }




        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }


        public void WriteSingleCoil(string Add, bool value)
        {
            OperationQueue.Enqueue(new PLCEnum
            {
                OperationType = PLCEnum.OperationTypeEnum.WriteSingleCoil,
                boolValue = value,
                M241Bit = Add
            });
        }

        public void WriteSingleRegister(int Add, short Value)
        {
            OperationQueue.Enqueue(new PLCEnum
            {
                OperationType = PLCEnum.OperationTypeEnum.WriteSingleRegsiter,
                intValue = Value,
                Address = Add
            });
        }

        public void WriteString(int Add, string Value)
        {
            OperationQueue.Enqueue(new PLCEnum
            {
                OperationType = PLCEnum.OperationTypeEnum.WriteString,
                stringValue = Value,
                Address = Add
            });

        }

        public void WriteFloat(int Add, float value)
        {
            OperationQueue.Enqueue(new PLCEnum
            {
                OperationType = PLCEnum.OperationTypeEnum.WriteFloat,
                floatvalue = value,
                Address = Add
            });
        }


        public bool ModbusService(string plcIpAddress, int plcPort)
        {
            _modbusClient = new ModbusTcpClient();
            _modbusClient.Connect(plcIpAddress, ModbusEndianness.LittleEndian);
            return _modbusClient.IsConnected;
        }


        private ushort[] WriteFloat(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] registers = new ushort[2];

            registers[0] = BitConverter.ToUInt16(bytes, 0);
            registers[1] = BitConverter.ToUInt16(bytes, 2);

            return registers;
        }


        private ushort[] WriteString(string value)
        {
            byte[] stringBytes = Encoding.ASCII.GetBytes(value);

            if (stringBytes.Length % 2 != 0)
            {
                Array.Resize(ref stringBytes, stringBytes.Length + 1);
            }

            ushort[] registers = new ushort[stringBytes.Length / 2];

            for (int i = 0; i < registers.Length; i++)
            {
                registers[i] = BitConverter.ToUInt16(stringBytes, i * 2);
            }
            return registers;
        }

        public float GetFloat(int registerIndex)
        {
            if (registerIndex < 0 || registerIndex + 1 >= PLCWord.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(registerIndex), "Invalid register index.");
            }

            int i = PLCWord[400];
            int j = PLCWord[401];

            byte[] bytes = new byte[4];

            BitConverter.GetBytes((ushort)PLCWord[registerIndex]).CopyTo(bytes, 0);
            BitConverter.GetBytes((ushort)PLCWord[registerIndex + 1]).CopyTo(bytes, 2);

            return BitConverter.ToSingle(bytes, 0);
        }

        public int GetInt32(int registerIndex, bool isBigEndian = false)
        {
            // Validate that there are at least two registers available.
            if (registerIndex < 0 || registerIndex + 1 >= PLCWord.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(registerIndex), "Invalid register index.");
            }

            // Prepare a 4-byte array to hold the combined bytes from two 16-bit registers.
            byte[] bytes = new byte[4];

            // Read two 16-bit registers.
            ushort reg1 = (ushort)PLCWord[registerIndex];
            ushort reg2 = (ushort)PLCWord[registerIndex + 1];

            // Convert each register to its 2-byte representation.
            byte[] reg1Bytes = BitConverter.GetBytes(reg1);
            byte[] reg2Bytes = BitConverter.GetBytes(reg2);

            // If the PLC sends data in big-endian format, reverse the byte order of each register.
            if (isBigEndian)
            {
                Array.Reverse(reg1Bytes);
                Array.Reverse(reg2Bytes);
            }

            // Combine the two registers into a single 4-byte array.
            // Here, we assume that the first register is the high-order word and the second is the low-order word.
            reg1Bytes.CopyTo(bytes, 0);
            reg2Bytes.CopyTo(bytes, 2);

            // If instead your PLC sends the low-order word first, use the following order:
            // reg2Bytes.CopyTo(bytes, 0);
            // reg1Bytes.CopyTo(bytes, 2);

            // Convert the combined 4-byte array into a 32-bit integer.
            return BitConverter.ToInt32(bytes, 0);
        }


        public string GetString(int startRegister, int length)
        {
            length = 16;
            if (startRegister < 0 || startRegister + (length / 2) >= PLCWord.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startRegister), "Invalid register index.");
            }

            // Create a byte array to hold the string data
            byte[] bytes = new byte[length];

            // Convert each 16-bit register into two bytes
            for (int i = 0; i < length / 2; i++)
            {
                BitConverter.GetBytes((ushort)PLCWord[startRegister + i]).CopyTo(bytes, i * 2);
            }

            // Convert byte array to a string (assuming ASCII encoding)
            return Encoding.ASCII.GetString(bytes).TrimEnd('\0','\n','\r'); // Trim null characters
        }

        public void Retry()
        {
            if (ConnectionError)
            {
                if (!_modbusClient.IsConnected)
                {
                    try
                    {
                        _modbusClient.Connect(ip, ModbusEndianness.BigEndian);
                    }
                    catch
                    {
                        isInitalised = false;
                        status = "Modbus connection failed.";
                    }
                }

                _ = Task.Run(() => FetchDataContinuously(_cancellationTokenSource.Token));
            }
        }

        private async Task FetchDataContinuously(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested && _modbusClient != null)
                {
                    if (true)
                    {
                    //    int totalBits = 2000;
                    //    int chunkSize = 125;  

                    //    for (int startAddress = 0; startAddress < totalBits; startAddress += chunkSize)
                    //    {
                    //        int bitsToRead = Math.Min(chunkSize, totalBits - startAddress);   

                    //        Memory<byte> coilData = await _modbusClient.ReadCoilsAsync(1, (ushort)startAddress, (ushort)bitsToRead, cancellationToken);
                    //        byte[] coilBytes = coilData.ToArray();

                    //        for (int i = 0; i < bitsToRead; i++)
                    //        {
                    //            int bitIndex = startAddress + i;
                    //            PLCbit[bitIndex] = ((coilBytes[i / 8] >> (i % 8)) & 1) != 0;
                    //        }
                    //    }

                        try
                        {
                            int totalRegisters = 900;
                            int chunkSize = 100;
                            DateTime dt = DateTime.Now;

                            for (int i = 0; i < totalRegisters; i += chunkSize)
                            {
                                System.Memory<short> memoryValues = await _modbusClient.ReadHoldingRegistersAsync<short>(1, (ushort)i, (ushort)chunkSize);

                                short[] blockValues = memoryValues.ToArray();

                                Array.Copy(blockValues, 0, PLCWord, i, blockValues.Length);
                                await Task.Delay(2);
                            }

                            for(int j = 0; j < PLCWord.Length; j++)
                            {
                                Array.Copy(IntToBoolArray(PLCWord[j]), 0, PLCbit, j * 16, 16);
                            }



                            //          Console.WriteLine(DateTime.Now - dt);
                            for (int i = 0; i < OperationQueue.Count; i++)
                            {
                                if (OperationQueue.TryDequeue(out PLCEnum queue))
                                {

                                    switch (queue.OperationType)
                                    {
                                        case (PLCEnum.OperationTypeEnum.WriteSingleCoil):
                                            
                                            ushort v = (ushort)(int.Parse(queue.M241Bit));
                                             await  _modbusClient.WriteSingleCoilAsync(1,v,queue.boolValue);

                                            break;
                                        case (PLCEnum.OperationTypeEnum.WriteSingleRegsiter):

                                            await _modbusClient.WriteSingleRegisterAsync(1, queue.Address, queue.intValue);

                                            break;
                                        case (PLCEnum.OperationTypeEnum.WriteFloat):

                                            await _modbusClient.WriteMultipleRegistersAsync(1, (ushort)queue.Address, WriteFloat(queue.floatvalue));

                                            break;
                                        case (PLCEnum.OperationTypeEnum.WriteString):

                                            await _modbusClient.WriteMultipleRegistersAsync(1, (ushort)queue.Address, WriteString(queue.stringValue));

                                            break;
                                        case (PLCEnum.OperationTypeEnum.WriteInt32):

                                            //await _modbusClient.WriteMultipleRegistersAsync(1, (ushort)queue.Address, (988033));

                                            break;

                                    }
                                }
                            }

                            _ishealthy = true;
                            ConnectionError = false;
                            Refreshed();
                            ConnectionEstablished = true;

                        }
                        catch (Exception ex)
                        {
                            _ishealthy = false;
                            ConnectionError = true;
                            ConnectionEstablished = false;

                        }
                        
                        await Task.Delay(50, cancellationToken);
                        status = "conncted";
                        
                    }
                    else
                    {
                        _ishealthy = false;
                        ConnectionError = false;
                        ConnectionEstablished = false;
                        MessageBox.Show("Modbus Failed");
                    }

                    if (ConnectionError)
                    {
                        DataRefreshed?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                    await Task.Delay(10);
                }
            }
            catch (TaskCanceledException)
            {
                status = "Fetching loop was canceled gracefully.";
                _ishealthy = false;
                ConnectionError = true;
                ConnectionEstablished = false;
            }
            catch (Exception ex)
            {

                _ishealthy = false;
                ConnectionError = false;
                MessageBox.Show("PLC Comm Error: " + ex.Message);
                status = "PLC Connection Error";
                ConnectionEstablished = false;
            }
            finally
            {

            }

           
            await Task.Delay(50);
           

        }

        public static bool[] IntToBoolArray(short value)
        {
            bool[] bits = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                bits[i] = (value & (1 << i)) != 0;
            }
            return bits;
        }


        private async Task M241WriteSingleCoil(string bitAddress, bool condition)
        {
            string[] parts = bitAddress.Split('.');
            int RegIndex = int.Parse(parts[0]); // 0 or 1
            int bitIndex = int.Parse(parts[1]);  // 0 to 7

            if (RegIndex % 2 == 0)
            {

                RegIndex = RegIndex / 2;
            }
            else
            {

                RegIndex = RegIndex - 1;
                RegIndex = RegIndex / 2;
                bitIndex = bitIndex + 8;
            }

            // Read current 16-bit word
            Memory<byte> raw = await _modbusClient.ReadHoldingRegistersAsync(1, (ushort)RegIndex, 1);
            ushort word = BinaryPrimitives.ReadUInt16BigEndian(raw.Span);

            // Modify the specific bit
            if (condition)
                word = (ushort)(word | (1 << bitIndex));     // Set bit
            else
                word = (ushort)(word & ~(1 << bitIndex));    // Clear bit

            // Write back updated word

            await _modbusClient.WriteSingleRegisterAsync((byte)1, (ushort)RegIndex, word);
        }
    
          
            private void Refreshed()
            {
                DataRefreshed?.Invoke(this, EventArgs.Empty);
            }
        

    }

}
