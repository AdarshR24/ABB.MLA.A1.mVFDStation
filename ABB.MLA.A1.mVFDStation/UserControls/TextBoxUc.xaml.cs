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
    /// Interaction logic for TextBoxUc.xaml
    /// </summary>
    public partial class TextBoxUc : UserControl
    {
        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(TextBoxUc), new PropertyMetadata(Brushes.Gray));


        public string value
        {
            get { return (string)GetValue(valueProperty); }
            set { SetValue(valueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty valueProperty =
            DependencyProperty.Register("value", typeof(string), typeof(TextBoxUc), new PropertyMetadata("Text"));



        public string BoxWidth
        {
            get { return (string)GetValue(BoxWidthProperty); }
            set { SetValue(BoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoxWidthProperty =
            DependencyProperty.Register("BoxWidth", typeof(string), typeof(TextBoxUc), new PropertyMetadata("50"));



        public TextBoxUc()
        {
            InitializeComponent();
        }
    }
}
