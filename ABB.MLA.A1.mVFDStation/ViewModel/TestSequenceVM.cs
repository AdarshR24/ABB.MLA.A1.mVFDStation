using ABB.MLA.A1.mVFDStation.Commands;
using ABB.MLA.A1.mVFDStation.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ABB.MLA.A1.mVFDStation.ViewModel
{
   public class TestSequenceVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ModbusClass mb;
        public DBConnect dbconn { get; set; }
        DataTable dt = new DataTable();
        public ICommand InitButtonCommand { get; set; }

        public ICommand mVButtonCommand { get; set; }

        public ICommand FDButtonCommand { get; set; }

        private Visibility mvVisible;

        public Visibility mVVisible
        {
            get { return mvVisible; }
            set
            {
                mvVisible = value;
                OnPropertyChanged(nameof(mVVisible));
            }
        }
        private Visibility fDVisible;

        public Visibility FDVisible
        {
            get { return fDVisible; }
            set
            {
                fDVisible = value;
                OnPropertyChanged(nameof(FDVisible));
            }
        }

        public TestSequenceVM(ModbusClass modbus, DBConnect Db) 
        {
            mb = modbus;
            dbconn = Db;
            mVButtonCommand = new RelayCommand(mVScreen);
            FDButtonCommand = new RelayCommand(FDScreen);
            mb.DataRefreshed += Mb_DataRefreshed;
        }

        private void Mb_DataRefreshed(object? sender, EventArgs e)
        {
           
        }
        //void Trip_Validation(int SinglePole1, int SinglePole1Res, int SinglePole2, int SinglePole2Res, int SinglePole3, int SinglePole3Res, int SinglePole4, int SinglePole4Res, int TwoPole1, int TwoPole1Res, int TwoPole2, int TwoPole2Res)
        //{
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole1))
        //    {
        //        ResCheck(tb_trp1, App.ModbusTCP.ReadSingleCoil(SinglePole1Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole2))
        //    {
        //        ResCheck(tb_trp2, App.ModbusTCP.ReadSingleCoil(SinglePole2Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole3))
        //    {
        //        ResCheck(tb_trp3, App.ModbusTCP.ReadSingleCoil(SinglePole3Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole4))
        //    {
        //        ResCheck(tb_trp4, App.ModbusTCP.ReadSingleCoil(SinglePole4Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(TwoPole1))
        //    {
        //        ResCheck(tb_trp1, App.ModbusTCP.ReadSingleCoil(TwoPole1Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(TwoPole2))
        //    {
        //        ResCheck(tb_trp3, App.ModbusTCP.ReadSingleCoil(TwoPole2Res));
        //    }
        //}

        //void Endurance_Validation(int SinglePole1, int SinglePole1Res, int SinglePole2, int SinglePole2Res, int SinglePole3, int SinglePole3Res, int SinglePole4, int SinglePole4Res, int TwoPole1, int TwoPole1Res, int TwoPole2, int TwoPole2Res)
        //{
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole1))
        //    {
        //        ResCheck(tb_end1, App.ModbusTCP.ReadSingleCoil(SinglePole1Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole2))
        //    {
        //        ResCheck(tb_end2, App.ModbusTCP.ReadSingleCoil(SinglePole2Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole3))
        //    {
        //        ResCheck(tb_end3, App.ModbusTCP.ReadSingleCoil(SinglePole3Res));

        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(SinglePole4))
        //    {
        //        ResCheck(tb_end4, App.ModbusTCP.ReadSingleCoil(SinglePole4Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(TwoPole1))
        //    {
        //        ResCheck(tb_end1, App.ModbusTCP.ReadSingleCoil(TwoPole1Res));
        //    }
        //    if (App.ModbusTCP.ReadSingleCoil(TwoPole2))
        //    {
        //        ResCheck(tb_end3, App.ModbusTCP.ReadSingleCoil(TwoPole2Res));
        //    }
        //}
        void mVScreen()
        {
            mVVisible=Visibility.Visible;
            FDVisible=Visibility.Hidden;
        }

        void FDScreen()
        {
            mVVisible = Visibility.Hidden;
            FDVisible = Visibility.Visible;
        }
    }
}
