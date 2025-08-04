using ABB.MLA.A1.mVFDStation.HelperClasses;
using ABB.MLA.A1.mVFDStation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ABB.MLA.A1.mVFDStation.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _overallstationerrors;
        public LoadingStationVM LoadingData { get; set; }
        public UnloadingStationVM UnloadingData { get; set; }

        public TestSequenceVM TestData { get; set; }


        public string Overallstationerror
        {
            get { return _overallstationerrors; }
            set { 
                if(_overallstationerrors != value)
                {
                    _overallstationerrors = value;
                    OnPropertyChanged();
                }
            }
        }

      
        
        private ModbusClass mb;
        DataTable dt=new DataTable();

        public DBConnect db { get; set; }
        public HomeViewModel(ModbusClass MB)
        {
            mb = MB;
            ErrorHandler.Instance.OverallStationErrors.ErrorListUpdated += OverallStationErrors_ErrorListUpdated;
            db =  new DBConnect();
            mb.WriteSingleCoil("152", true);
            LoadingData=new LoadingStationVM(mb,db);
            UnloadingData = new UnloadingStationVM(mb, db);
            TestData = new TestSequenceVM(mb, db);
        }

        private void OverallStationErrors_ErrorListUpdated(object sender, EventArgs e)
        {
           Overallstationerror = ErrorHandler.Instance.OverallStationErrors.GetErrorString();
        }
    }
}
