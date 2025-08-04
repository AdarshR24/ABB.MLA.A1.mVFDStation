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
    /// Interaction logic for TestingUc.xaml
    /// </summary>
    public partial class TestingUc : UserControl
    {
        public TestSequenceVM StationDetails
        {
            get { return (TestSequenceVM)GetValue(StationDetailsProperty); }
            set { SetValue(StationDetailsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationDetails.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationDetailsProperty =
           DependencyProperty.Register("StationDetails", typeof(TestSequenceVM), typeof(TestingUc), new PropertyMetadata(null));
        public TestingUc()
        {
            InitializeComponent();
        }
    }
}
