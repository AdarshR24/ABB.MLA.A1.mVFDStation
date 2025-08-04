using ABB.MLA.A1.mVFDStation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ABB.MLA.A1.mVFDStation.UserControls
{
    /// <summary>
    /// Interaction logic for LoadingStationUc.xaml
    /// </summary>
    public partial class LoadingStationUc : UserControl
    {
        public LoadingStationVM StationDetails
        {
            get { return (LoadingStationVM)GetValue(StationDetailsProperty); }
            set { SetValue(StationDetailsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationDetails.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationDetailsProperty =
            DependencyProperty.Register("StationDetails", typeof(LoadingStationVM), typeof(LoadingStationUc), new PropertyMetadata(null));

        public ICommand InitButtonCommand
        {
            get { return (ICommand)GetValue(InitButtonCommandProperty); }
            set { SetValue(InitButtonCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitButtonCommandProperty =
            DependencyProperty.Register("InitButtonCommand", typeof(ICommand), typeof(LoadingStationUc), new PropertyMetadata(null));
        public LoadingStationUc()
        {
            InitializeComponent();
        }
    }
}
