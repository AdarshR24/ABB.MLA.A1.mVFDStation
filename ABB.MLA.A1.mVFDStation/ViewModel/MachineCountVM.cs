using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.MLA.A1.mVFDStation.ViewModel
{
    public class MachineCount_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CountModel_VM OnePole { get; set; }
        public CountModel_VM TwoPole { get; set; }



        public MachineCount_VM()
        {
            OnePole = new CountModel_VM();
            TwoPole = new CountModel_VM();

        }
    }
}
