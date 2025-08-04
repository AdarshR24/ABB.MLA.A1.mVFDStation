using ABB.MLA.A1.mVFDStation.Commands;
using ABB.MLA.A1.mVFDStation.HelperClasses;
using ABB.MLA.A1.mVFDStation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;


namespace ABB.MLA.A1.mVFDStation.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public NavigationVM Navigation { get; }

        private string currentshift;

        public string CurrentShift
        {
            get { return currentshift; }
            set { 
                if(currentshift != value)
                {
                    currentshift = value;
                    OnPropertyChanged();
                }
                }
        }

        public object CurrentPage => Navigation.CurrentPage;
        public ICommand HomeCommand { get; }
        public ICommand ManualCommand { get; }
        public ICommand DatabaseCommand { get; }
        public ICommand ReportCommand { get; }
        public ICommand DowngradeCommand { get; }
        public ICommand MaintenanceCommand { get; }
        public ICommand ParameterCommand { get; }

        public ModbusClass mb { get; set; }

        public HomeViewModel _Homeviewmodel { get; set; }

        public DBConnect db { get; set; }

        public MainWindowViewModel()
        {
          db = new DBConnect();

            // get PLC IP from here

            mb = new ModbusClass();
            mb.Initialize("192.168.13.20", 502);


            ViewModelCollection viewModelCollection = new ViewModelCollection(mb);
           




            Navigation = new NavigationVM(viewModelCollection,mb,db);
            Navigation.NavigateTo("Home");

            Navigation.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Navigation.CurrentPage))
                    OnPropertyChanged(nameof(CurrentPage));
            };

            HomeCommand = new RelayCommand(() => Navigation.NavigateTo("Home"));
            ManualCommand = new RelayCommand(() => Navigation.NavigateTo("Manual"));
            DatabaseCommand = new RelayCommand(() => Navigation.NavigateTo("Database"));
            ReportCommand = new RelayCommand(() => Navigation.NavigateTo("Report"));
            DowngradeCommand = new RelayCommand(() => Navigation.NavigateTo("Downgrade"));
            MaintenanceCommand = new RelayCommand(() => Navigation.NavigateTo("Maintenance"));
            ParameterCommand = new RelayCommand(() => Navigation.NavigateTo("Parameters"));
            ShiftCheck sf = new ShiftCheck(db);
            currentshift = sf.CurrentShift;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RelayCommand CloseCommand => new RelayCommand(() =>
        {
            Environment.Exit(0);
            //DecisionWindow decisionWindow = new DecisionWindow();
            //decisionWindow.Message("Are you sure you want to exit the application?");
            //decisionWindow.ShowDialog();
            //if (decisionWindow.Decision)
            //{
            //    mb.WriteSingleCoil("152", false); // Exit

            //    Environment.Exit(0);
            //}

        });

    }

}
