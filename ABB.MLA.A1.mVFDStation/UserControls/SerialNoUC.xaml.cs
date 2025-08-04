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
    /// Interaction logic for SerialNoUC.xaml
    /// </summary>
    public partial class SerialNoUC : UserControl
    {

        public string LabelName
        {
            get { return (string)GetValue(LabelNameProperty); }
            set { SetValue(LabelNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelNameProperty =
            DependencyProperty.Register("LabelName", typeof(string), typeof(SerialNoUC), new PropertyMetadata(""));



        public string SerialNo
        {
            get { return (string)GetValue(SerialNoProperty); }
            set { SetValue(SerialNoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SerialNo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SerialNoProperty =
            DependencyProperty.Register("SerialNo", typeof(string), typeof(SerialNoUC), new PropertyMetadata(""));




        public SerialNoUC()
        {
            InitializeComponent();
        }
    }
}
