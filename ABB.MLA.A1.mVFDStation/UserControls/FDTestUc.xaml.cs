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
    /// Interaction logic for FDTestUc.xaml
    /// </summary>
    public partial class FDTestUc : UserControl
    {
        public string F1Data
        {
            get { return (string)GetValue(F1DataProperty); }
            set { SetValue(F1DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty F1DataProperty =
            DependencyProperty.Register("F1Data", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public string F2Data
        {
            get { return (string)GetValue(F2DataProperty); }
            set { SetValue(F2DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty F2DataProperty =
            DependencyProperty.Register("F2Data", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public string D1Data
        {
            get { return (string)GetValue(D1DataProperty); }
            set { SetValue(D1DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty D1DataProperty =
            DependencyProperty.Register("D1Data", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public string D2Data
        {
            get { return (string)GetValue(D2DataProperty); }
            set { SetValue(D2DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty D2DataProperty =
            DependencyProperty.Register("D2Data", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public Brush F1Brush
        {
            get { return (Brush)GetValue(F1BrushProperty); }
            set { SetValue(F1BrushProperty, value); }
        }

        public static readonly DependencyProperty F1BrushProperty =
            DependencyProperty.Register("F1Brush", typeof(Brush), typeof(FDTestUc), new PropertyMetadata(Brushes.Transparent));

        public Brush F2Brush
        {
            get { return (Brush)GetValue(F2BrushProperty); }
            set { SetValue(F2BrushProperty, value); }
        }

        public static readonly DependencyProperty F2BrushProperty =
            DependencyProperty.Register("F2Brush", typeof(Brush), typeof(FDTestUc), new PropertyMetadata(Brushes.Transparent));

        public Brush D1Brush
        {
            get { return (Brush)GetValue(D1BrushProperty); }
            set { SetValue(D1BrushProperty, value); }
        }

        public static readonly DependencyProperty D1BrushProperty =
            DependencyProperty.Register("D1Brush", typeof(Brush), typeof(FDTestUc), new PropertyMetadata(Brushes.Transparent));

        public Brush D2Brush
        {
            get { return (Brush)GetValue(D2BrushProperty); }
            set { SetValue(D2BrushProperty, value); }
        }

        public static readonly DependencyProperty D2BrushProperty =
            DependencyProperty.Register("D2Brush", typeof(Brush), typeof(FDTestUc), new PropertyMetadata(Brushes.Transparent));

        public string FMin
        {
            get { return (string)GetValue(FMinProperty); }
            set { SetValue(FMinProperty, value); }
        }

        public static readonly DependencyProperty FMinProperty =
            DependencyProperty.Register("FMin", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public string FMax
        {
            get { return (string)GetValue(FMaxProperty); }
            set { SetValue(FMaxProperty, value); }
        }

        public static readonly DependencyProperty FMaxProperty =
            DependencyProperty.Register("FMax", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public string DMin
        {
            get { return (string)GetValue(DMinProperty); }
            set { SetValue(DMinProperty, value); }
        }

        public static readonly DependencyProperty DMinProperty =
            DependencyProperty.Register("DMin", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));

        public string DMax
        {
            get { return (string)GetValue(DMaxProperty); }
            set { SetValue(DMaxProperty, value); }
        }

        public static readonly DependencyProperty DMaxProperty =
            DependencyProperty.Register("DMax", typeof(string), typeof(FDTestUc), new PropertyMetadata(""));


        public FDTestUc()
        {
            InitializeComponent();
        }
    }
}
