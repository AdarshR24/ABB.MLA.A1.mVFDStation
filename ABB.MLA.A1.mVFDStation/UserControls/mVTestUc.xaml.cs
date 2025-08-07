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
    /// Interaction logic for mVTestUc.xaml
    /// </summary>
    public partial class mVTestUc : UserControl
    {
        public string P1
        {
            get { return (string)GetValue(P1Property); }
            set { SetValue(P1Property, value); }
        }

        public static readonly DependencyProperty P1Property =
            DependencyProperty.Register("P1", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public string P2
        {
            get { return (string)GetValue(P2Property); }
            set { SetValue(P2Property, value); }
        }

        public static readonly DependencyProperty P2Property =
            DependencyProperty.Register("P2", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public string P3
        {
            get { return (string)GetValue(P3Property); }
            set { SetValue(P3Property, value); }
        }

        public static readonly DependencyProperty P3Property =
            DependencyProperty.Register("P3", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public string P4
        {
            get { return (string)GetValue(P4Property); }
            set { SetValue(P4Property, value); }
        }

        public static readonly DependencyProperty P4Property =
            DependencyProperty.Register("P4", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public string MVMin
        {
            get { return (string)GetValue(MVMinProperty); }
            set { SetValue(MVMinProperty, value); }
        }

        public static readonly DependencyProperty MVMinProperty =
            DependencyProperty.Register("MVMin", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public string MVMax
        {
            get { return (string)GetValue(MVMaxProperty); }
            set { SetValue(MVMaxProperty, value); }
        }

        public static readonly DependencyProperty MVMaxProperty =
            DependencyProperty.Register("MVMax", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public string MVCurrent
        {
            get { return (string)GetValue(MVCurrentProperty); }
            set { SetValue(MVCurrentProperty, value); }
        }

        public static readonly DependencyProperty MVCurrentProperty =
            DependencyProperty.Register("MVCurrent", typeof(string), typeof(mVTestUc), new PropertyMetadata(""));

        public Brush P1Brush
        {
            get { return (Brush)GetValue(P1BrushProperty); }
            set { SetValue(P1BrushProperty, value); }
        }

        public static readonly DependencyProperty P1BrushProperty =
            DependencyProperty.Register("P1Brush", typeof(Brush), typeof(mVTestUc), new PropertyMetadata(Brushes.Transparent));

        public Brush P2Brush
        {
            get { return (Brush)GetValue(P2BrushProperty); }
            set { SetValue(P2BrushProperty, value); }
        }

        public static readonly DependencyProperty P2BrushProperty =
            DependencyProperty.Register("P2Brush", typeof(Brush), typeof(mVTestUc), new PropertyMetadata(Brushes.Transparent));

        public Brush P3Brush
        {
            get { return (Brush)GetValue(P3BrushProperty); }
            set { SetValue(P3BrushProperty, value); }
        }

        public static readonly DependencyProperty P3BrushProperty =
            DependencyProperty.Register("P3Brush", typeof(Brush), typeof(mVTestUc), new PropertyMetadata(Brushes.Transparent));

        public Brush P4Brush
        {
            get { return (Brush)GetValue(P4BrushProperty); }
            set { SetValue(P4BrushProperty, value); }
        }

        public static readonly DependencyProperty P4BrushProperty =
            DependencyProperty.Register("P4Brush", typeof(Brush), typeof(mVTestUc), new PropertyMetadata(Brushes.Transparent));

        public mVTestUc()
        {
            InitializeComponent();
        }
    }
}
