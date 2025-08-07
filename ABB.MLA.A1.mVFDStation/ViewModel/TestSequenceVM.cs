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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        bool FlagEdr = false, FlagTrp = false, FlagFd1 = false, FlagFd2 = false,
            FlagChangeovermV = false, FlagChangeoverFd=false, mvinpreogress=false;
        mVSequence mvcheck;
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
        bool ABc = false;

        private string serialNo1;
        public string SerialNo1
        {
            get => serialNo1;
            set
            {
                if (serialNo1 != value)
                {
                    serialNo1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string serialNo2;
        public string SerialNo2
        {
            get => serialNo2;
            set
            {
                if (serialNo2 != value)
                {
                    serialNo2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string serialNo3;
        public string SerialNo3
        {
            get => serialNo3;
            set
            {
                if (serialNo3 != value)
                {
                    serialNo3 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string serialNo4;
        public string SerialNo4
        {
            get => serialNo4;
            set
            {
                if (serialNo4 != value)
                {
                    serialNo4 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string p1;
        public string P1
        {
            get => p1;
            set
            {
                if (p1 != value)
                {
                    p1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string p2;
        public string P2
        {
            get => p2;
            set
            {
                if (p2 != value)
                {
                    p2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string p3;
        public string P3
        {
            get => p3;
            set
            {
                if (p3 != value)
                {
                    p3 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string p4;
        public string P4
        {
            get => p4;
            set
            {
                if (p4 != value)
                {
                    p4 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string mvMin;
        public string MVMin
        {
            get => mvMin;
            set
            {
                if (mvMin != value)
                {
                    mvMin = value;
                    OnPropertyChanged();
                }
            }
        }

        private string mvMax;
        public string MVMax
        {
            get => mvMax;
            set
            {
                if (mvMax != value)
                {
                    mvMax = value;
                    OnPropertyChanged();
                }
            }
        }

        private string mvCurrent;
        public string MVCurrent
        {
            get => mvCurrent;
            set
            {
                if (mvCurrent != value)
                {
                    mvCurrent = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush p1Brush;
        public Brush P1Brush
        {
            get { return p1Brush; }
            set
            {
                if (p1Brush != value)
                {
                    p1Brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush p2Brush;
        public Brush P2Brush
        {
            get { return p2Brush; }
            set
            {
                if (p2Brush != value)
                {
                    p2Brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush p3Brush;
        public Brush P3Brush
        {
            get { return p3Brush; }
            set
            {
                if (p3Brush != value)
                {
                    p3Brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush p4Brush;
        public Brush P4Brush
        {
            get { return p4Brush; }
            set
            {
                if (p4Brush != value)
                {
                    p4Brush = value;
                    OnPropertyChanged();
                }
            }
        }
        private string edrRes1;
        public string EdrRes1
        {
            get { return edrRes1; }
            set
            {
                if (edrRes1 != value)
                {
                    edrRes1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string edrRes2;
        public string EdrRes2
        {
            get { return edrRes2; }
            set
            {
                if (edrRes2 != value)
                {
                    edrRes2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string edrRes3;
        public string EdrRes3
        {
            get { return edrRes3; }
            set
            {
                if (edrRes3 != value)
                {
                    edrRes3 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string edrRes4;
        public string EdrRes4
        {
            get { return edrRes4; }
            set
            {
                if (edrRes4 != value)
                {
                    edrRes4 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string tripRes1;
        public string TripRes1
        {
            get { return tripRes1; }
            set
            {
                if (tripRes1 != value)
                {
                    tripRes1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string tripRes2;
        public string TripRes2
        {
            get { return tripRes2; }
            set
            {
                if (tripRes2 != value)
                {
                    tripRes2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string tripRes3;
        public string TripRes3
        {
            get { return tripRes3; }
            set
            {
                if (tripRes3 != value)
                {
                    tripRes3 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string tripRes4;
        public string TripRes4
        {
            get { return tripRes4; }
            set
            {
                if (tripRes4 != value)
                {
                    tripRes4 = value;
                    OnPropertyChanged();
                }
            }
        }
        private Brush edrBrush1;
        public Brush EdrBrush1
        {
            get { return edrBrush1; }
            set
            {
                if (edrBrush1 != value)
                {
                    edrBrush1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush edrBrush2;
        public Brush EdrBrush2
        {
            get { return edrBrush2; }
            set
            {
                if (edrBrush2 != value)
                {
                    edrBrush2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush edrBrush3;
        public Brush EdrBrush3
        {
            get { return edrBrush3; }
            set
            {
                if (edrBrush3 != value)
                {
                    edrBrush3 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush edrBrush4;
        public Brush EdrBrush4
        {
            get { return edrBrush4; }
            set
            {
                if (edrBrush4 != value)
                {
                    edrBrush4 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush tripBrush1;
        public Brush TripBrush1
        {
            get { return tripBrush1; }
            set
            {
                if (tripBrush1 != value)
                {
                    tripBrush1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush tripBrush2;
        public Brush TripBrush2
        {
            get { return tripBrush2; }
            set
            {
                if (tripBrush2 != value)
                {
                    tripBrush2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush tripBrush3;
        public Brush TripBrush3
        {
            get { return tripBrush3; }
            set
            {
                if (tripBrush3 != value)
                {
                    tripBrush3 = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush tripBrush4;
        public Brush TripBrush4
        {
            get { return tripBrush4; }
            set
            {
                if (tripBrush4 != value)
                {
                    tripBrush4 = value;
                    OnPropertyChanged();
                }
            }
        }


        private string f1Text;
        public string F1Text
        {
            get { return f1Text; }
            set
            {
                if (f1Text != value)
                {
                    f1Text = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush f1Brush;
        public Brush F1Brush
        {
            get { return f1Brush; }
            set
            {
                if (f1Brush != value)
                {
                    f1Brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private string d1Text;
        public string D1Text
        {
            get { return d1Text; }
            set
            {
                if (d1Text != value)
                {
                    d1Text = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush d1Brush;
        public Brush D1Brush
        {
            get { return d1Brush; }
            set
            {
                if (d1Brush != value)
                {
                    d1Brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private string f2Text;
        public string F2Text
        {
            get { return f2Text; }
            set
            {
                if (f2Text != value)
                {
                    f2Text = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush f2Brush;
        public Brush F2Brush
        {
            get { return f2Brush; }
            set
            {
                if (f2Brush != value)
                {
                    f2Brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private string d2Text;
        public string D2Text
        {
            get { return d2Text; }
            set
            {
                if (d2Text != value)
                {
                    d2Text = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush d2Brush;
        public Brush D2Brush
        {
            get { return d2Brush; }
            set
            {
                if (d2Brush != value)
                {
                    d2Brush = value;
                    OnPropertyChanged();
                }
            }
        }
        mVSource ms =new mVSource("COM1");
        public TestSequenceVM(ModbusClass modbus, DBConnect Db) 
        {
            mb = modbus;
            dbconn = Db;
            mvcheck=new mVSequence(ms,modbus,"COM7");
            mVButtonCommand = new RelayCommand(mVScreen);
            FDButtonCommand = new RelayCommand(FDScreen);
            mb.DataRefreshed += Mb_DataRefreshed;
            F1Brush=Brushes.Gray;
            F2Brush = Brushes.Gray;
            D1Brush = Brushes.Gray;
            D2Brush = Brushes.Gray;
            P1Brush = Brushes.Gray;
            P2Brush = Brushes.Gray;
            P3Brush = Brushes.Gray;
            P4Brush = Brushes.Gray;
            P1 = "0";
        }

        private void Mb_DataRefreshed(object? sender, EventArgs e)
        {
            if (mb.PLCbit[415] && !mvinpreogress)
            {
                mvinpreogress = true;
                mvcheck.Start();
            }
            if (!mb.PLCbit[415] && mvinpreogress)
            {
                mvinpreogress = false;
                mvcheck.Stop();
            }

            P1 = mvcheck.R;
            P2 = mvcheck.Y;
            P3 = mvcheck.B;
            P4 = mvcheck.N;

            #region Trip And Endurance
            if (mb.PLCbit[476] && !FlagEdr)
            {
                Endurance_Validation(460, 472, 461, 473, 463, 474, 464, 475, 462, 470, 465, 471);
                mb.WriteSingleCoil("476", false);
                FlagEdr = true;
            }
            if (!mb.PLCbit[476] && FlagEdr)
            {
                FlagEdr = false;
            }
            if (mb.PLCbit[486] && !FlagTrp)
            {
                Trip_Validation(460, 482, 461, 483, 463, 484, 464, 485, 462, 480, 465, 481);
                mb.WriteSingleCoil("486", false);
                FlagTrp = true;
            }
            if (!mb.PLCbit[486] && FlagTrp)
            {
                FlagTrp = false;
            }
            #endregion

            #region FD Test
            if (mb.PLCbit[494] && !FlagFd1)
            {
                F1Text = mb.GetFloat(50).ToString("0.00");//force
                D1Text = mb.GetFloat(52).ToString("0.00");//distance
                FDResCheck(F1Brush,D1Brush, 492, 493, 490, 491);
                mb.WriteSingleCoil("494", false);
                FlagFd1 = true;
            }
            if (!mb.PLCbit[494] && FlagFd1)
            {
                FlagFd1 = false;
            }
            if (mb.PLCbit[499] && !FlagFd2)
            {
                F2Text = mb.GetFloat(54).ToString("0.00");//force
                D2Text = mb.GetFloat(56).ToString("0.00");//distance
                FDResCheck(F2Brush, D2Brush, 497, 498, 495, 496);
                mb.WriteSingleCoil("499", false);
                FlagFd2 = true;
            }
            if (!mb.PLCbit[499] && FlagFd2)
            {
                FlagFd2 = false;
            }
            #endregion

            #region ChangeOver
            if (mb.PLCbit[502] && !FlagChangeovermV)
            {
                FlagChangeovermV = true;
                mb.WriteSingleCoil("502", false);
                mVVisible = Visibility.Visible;
                FDVisible = Visibility.Hidden;
            }
            if (!mb.PLCbit[502] && FlagChangeovermV)
            {
                FlagChangeovermV = false;
            }
            if (mb.PLCbit[503] && !FlagChangeoverFd)
            {
                FlagChangeoverFd = true;
                mb.WriteSingleCoil("503", false);
                mVVisible = Visibility.Hidden;
                FDVisible = Visibility.Visible;
            }
            if (!mb.PLCbit[503] && FlagChangeoverFd)
            {
                FlagChangeoverFd = false;
            }
            #endregion

            SerialNo1 = "1234567899876543";
            SerialNo2 = "1234567899876543";
            SerialNo3 = "1234567899876543";
            SerialNo4 = "1234567899876543";
        }

        void FDResCheck(Brush Tb1,Brush Tb2, int Pass, int Fail, int FColorChange, int DColorChange)
        {
            if (mb.PLCbit[Pass])
            {
                mb.WriteSingleCoil(Pass.ToString(), false);
                if (mb.PLCbit[FColorChange])
                {
                    Tb1 = Brushes.Green;
                }
                else
                {
                    Tb1 = Brushes.Red;
                }
                if (mb.PLCbit[DColorChange])
                {
                    Tb2 = Brushes.Green;
                }
                else
                {
                    Tb2 = Brushes.Red;
                }
            }
            if (mb.PLCbit[Fail])
            {
                mb.WriteSingleCoil(Fail.ToString(), false);
                if (mb.PLCbit[FColorChange])
                {
                    Tb1 = Brushes.Green;
                }
                else
                {
                    Tb1 = Brushes.Red;
                }
                if (mb.PLCbit[DColorChange])
                {
                    Tb2 = Brushes.Green;
                }
                else
                {
                    Tb2 = Brushes.Red;
                }
            }

        }

        void Trip_Validation(int SinglePole1, int SinglePole1Res, int SinglePole2, int SinglePole2Res, int SinglePole3, int SinglePole3Res, int SinglePole4, int SinglePole4Res, int TwoPole1, int TwoPole1Res, int TwoPole2, int TwoPole2Res)
        {
            if (mb.PLCbit[SinglePole1])
            {
                ResCheck(TripRes1, mb.PLCbit[SinglePole1Res],TripBrush1);
            }
            if (mb.PLCbit[SinglePole2])
            {
                ResCheck(TripRes2, mb.PLCbit[SinglePole2Res],TripBrush2);
            }
            if (mb.PLCbit[SinglePole3])
            {
                ResCheck(TripRes3, mb.PLCbit[SinglePole3Res],TripBrush3);
            }
            if (mb.PLCbit[SinglePole4])
            {
                ResCheck(TripRes4, mb.PLCbit[SinglePole4Res],TripBrush4);
            }
            if (mb.PLCbit[TwoPole1])
            {
                ResCheck(TripRes1, mb.PLCbit[TwoPole1Res],TripBrush1);
            }
            if (mb.PLCbit[TwoPole2])
            {
                ResCheck(TripRes3, mb.PLCbit[TwoPole2Res],TripBrush3);
            }
        }

        void Endurance_Validation(int SinglePole1, int SinglePole1Res, int SinglePole2, int SinglePole2Res, int SinglePole3, int SinglePole3Res, int SinglePole4, int SinglePole4Res, int TwoPole1, int TwoPole1Res, int TwoPole2, int TwoPole2Res)
        {
            if (mb.PLCbit[SinglePole1])
            {
                ResCheck(EdrRes1, mb.PLCbit[SinglePole1Res],EdrBrush1);
            }
            if (mb.PLCbit[SinglePole2])
            {
                ResCheck(EdrRes2, mb.PLCbit[SinglePole2Res], EdrBrush2);
            }
            if (mb.PLCbit[SinglePole3])
            {
                ResCheck(EdrRes3, mb.PLCbit[SinglePole3Res], EdrBrush3);
            }
            if (mb.PLCbit[SinglePole4])
            {
                ResCheck(EdrRes4, mb.PLCbit[SinglePole4Res], EdrBrush4);
            }
            if (mb.PLCbit[TwoPole1])
            {
                ResCheck(EdrRes1, mb.PLCbit[TwoPole1Res], EdrBrush1);
            }
            if (mb.PLCbit[TwoPole2])
            {
                ResCheck(EdrRes3, mb.PLCbit[TwoPole2Res],EdrBrush3);
            }
        }

        void ResCheck(string Res, bool Value,Brush brush)
        {
            if (Value)
            {
                Res = "PASS";
                brush = Brushes.Green;
            }
            else
            {
                Res = "FAIL";
                brush = Brushes.Red;
            }
        }

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
