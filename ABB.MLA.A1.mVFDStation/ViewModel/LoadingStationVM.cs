using ABB.MLA.A1.mVFDStation.Commands;
using ABB.MLA.A1.mVFDStation.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ABB.MLA.A1.mVFDStation.ViewModel
{
   public class LoadingStationVM : INotifyPropertyChanged
   {
        private ModbusClass mb;
        public event EventHandler RunWihoutFingerReqEvent;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        AlarmLogger alarmLogger;
        public DBConnect dbconn { get; set; }
        DataTable dt =new DataTable();
        public ICommand InitButtonCommand { get; set; }
        private string _statusmesage;

        public string StatusMessage
        {
            get { return _statusmesage; }
            set
            {
                if (value != _statusmesage)
                {
                    _statusmesage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }
        private int plcstatuscode = -1;
        public int PLCStatusCode
        {
            get { return plcstatuscode; }
            set
            {

                if (plcstatuscode != value)
                {
                    plcstatuscode = value;
                    StatusMessage = updateStatusMessage(plcstatuscode);
                    OnPropertyChanged();

                }

            }
        }
        private string updateStatusMessage(int val)
        {
            if (dt.Rows?.Count >= val)
            {
                StatusMessage = dt.Rows[val]["Description"].ToString();
            }
            else if (dt.Rows?.Count < val)
            {
                StatusMessage = "ERROR: Excess Value from PLC";
            }
            else
            {
                StatusMessage = "ERROR: Data Not Loaded";
            }
            return StatusMessage;
        }
        public LoadingStationVM(ModbusClass modbus,DBConnect Db) 
        {
          mb=modbus;
          dbconn = Db;
          InitButtonCommand = new RelayCommand(Initialize);
          dt = dbconn.Select("Select * from LoadingStatusCodes");
          mb.DataRefreshed += Mb_DataRefreshed;

        }

        private void Mb_DataRefreshed(object sender, EventArgs e)
        {
            PLCStatusCode = mb.PLCWord[810];
        }
        public void Initialize()
        {
            mb.WriteSingleCoil("432", true);
            Console.WriteLine("Station Loading Status Initialized");
        }

    }
}
