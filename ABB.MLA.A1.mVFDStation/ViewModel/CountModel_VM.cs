using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.MLA.A1.mVFDStation.ViewModel
{
    public class CountModel_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private int pass;
        public int Pass
        {
            get { return pass; }
            set
            {
                if (pass != value)
                {
                    pass = value;
                    OnPropertyChanged(nameof(Pass));
                }
            }
        }

        private int qFpy;
        public int QFpy
        {
            get { return qFpy; }
            set
            {
                if (qFpy != value)
                {
                    qFpy = value;
                    OnPropertyChanged(nameof(QFpy));
                }
            }
        }

        private int fpy;

        public int Fpy
        {
            get { return fpy; }
            set
            {
                if (fpy != value)
                {
                    fpy = value;
                    OnPropertyChanged(nameof(Fpy));
                }
            }
        }

        private int total;
        public int Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public CountModel_VM()
        {
            Pass = 0;
            Total = 0;
        }
    }
}
