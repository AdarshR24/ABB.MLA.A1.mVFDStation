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
    /// Interaction logic for FPYCountUC.xaml
    /// </summary>
    public partial class FPYCountUC : UserControl
    {



        public MachineCount_VM Pole
        {
            get { return (MachineCount_VM)GetValue(PoleProperty); }
            set { SetValue(PoleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pole.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PoleProperty =
            DependencyProperty.Register("Pole", typeof(MachineCount_VM), typeof(FPYCountUC), new PropertyMetadata(null));




        public string poleContent
        {
            get { return (string)GetValue(poleContentProperty); }
            set { SetValue(poleContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for poleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty poleContentProperty =
            DependencyProperty.Register("poleContent", typeof(string), typeof(FPYCountUC), new PropertyMetadata(""));


        public FPYCountUC()
        {
            InitializeComponent();
        }
    }
}
