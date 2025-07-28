using ABB.MLA.A1.mVFDStation.HelperClasses;
using ABB.MLA.A1.mVFDStation.Model;
using ABB.MLA.A1.mVFDStation.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ABB.MLA.A1.mVFDStation.ViewModel
{
    public class NavigationVM : INotifyPropertyChanged
    {

        public ObservableCollection<Visibility>  Visiblities { get; set; }




        private object _currentPage;
        public object CurrentPage
        {
            get => _currentPage;
            set { 
                _currentPage = value; 
                OnPropertyChanged();
            }
        }

        ModbusClass _modbusClass;

        Home home;

        ViewModelCollection _viewModelCollection;


        DBConnect db;
        public NavigationVM(ViewModelCollection vm, ModbusClass mb,DBConnect db)
        {
            _viewModelCollection = vm;
            _modbusClass = mb;
            this.db = db;
            home = new Home(_viewModelCollection.HomeViewModel, _modbusClass);
            Visiblities = new ObservableCollection<Visibility> 
            {
                Visibility.Visible,
                Visibility.Hidden,
                Visibility.Hidden,
                Visibility.Hidden,
                Visibility.Hidden,
                Visibility.Hidden,
                Visibility.Hidden,
                Visibility.Hidden,
                Visibility.Hidden,
            
            };
        }

        public void NavigateTo(string pagename)
        {
            if (pagename != "Home")
            {
                CurrentPage = GetPage(pagename);
                //Login login = new Login(pagename);
                //login.ShowDialog();

                //if (login.LVM.LoggedIn)
                //{
                //}
            }
            else
            {
                CurrentPage = GetPage(pagename);
            }
        }

        public void ClearAll()
        {
            for (int i = 0; i < Visiblities.Count; i++)
            {
                Visiblities[i] = Visibility.Hidden;
            }
        }

        public object GetPage(string PageName)
        {
            ClearAll();
            switch (PageName)
            {
                case "Home":
                    Visiblities[0] = Visibility.Visible;
                    _modbusClass.WriteSingleCoil("152", true);//Auto Mode
                    return home;
                //case "Manual":
                //    Visiblities[1] = Visibility.Visible;
                //    _modbusClass.WriteSingleCoil("152", false);//Auto Mode
                //    return new ManualView(db,_modbusClass);
                //case "Database":
                //    Visiblities[3] = Visibility.Visible;
                //    _modbusClass.WriteSingleCoil("152", false);//Auto Mode
                //    return new DatabaseView(databaseVM);
                //case "Report":
                //    Visiblities[5] = Visibility.Visible;
                //    _modbusClass.WriteSingleCoil("152", false);//Auto Mode
                //    return new Report(db);
                //case "Downgrade":
                //    Visiblities[4] = Visibility.Visible;
                //    _modbusClass.WriteSingleCoil("152", false);//Auto Mode
                //    return new DowngradeView(db,_modbusClass);
                //case "Maintenance":
                //    Visiblities[7] = Visibility.Visible;
                //    _modbusClass.WriteSingleCoil("152", false);//Auto Mode
                //    return new MaintenanceView(db,_modbusClass);
                //case "Parameters":
                //    Visiblities[2] = Visibility.Visible;
                //    _modbusClass.WriteSingleCoil("152", false);//Auto Mode
                //    return new ParameterView();
                default:
                    return null;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
