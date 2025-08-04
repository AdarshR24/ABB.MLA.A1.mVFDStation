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
    /// Interaction logic for StatucUc.xaml
    /// </summary>
    public partial class StatusUc : UserControl
    {



        public string LabelName
        {
            get { return (string)GetValue(LabelNameProperty); }
            set { SetValue(LabelNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelNameProperty =
            DependencyProperty.Register("LabelName", typeof(string), typeof(StatusUc), new PropertyMetadata(""));



        private void InitButtonCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private Visibility InitButtonVisbility
        {
            get { return (Visibility)GetValue(InitButtonVisbilityProperty); }
            set { SetValue(InitButtonVisbilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitButtonVisbility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitButtonVisbilityProperty =
            DependencyProperty.Register("InitButtonVisbility", typeof(Visibility), typeof(StatusUc), new PropertyMetadata(Visibility.Visible));




        public ICommand InitButtonCommand
        {
            get { return (ICommand)GetValue(InitButtonCommandProperty); }
            set { SetValue(InitButtonCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitButtonCommandProperty =
            DependencyProperty.Register("InitButtonCommand", typeof(ICommand), typeof(StatusUc), new PropertyMetadata(null));



        public string StatusMessage
        {
            get { return (string)GetValue(StatusMessageProperty); }
            set { SetValue(StatusMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusMessageProperty =
            DependencyProperty.Register("StatusMessage", typeof(string), typeof(StatusUc), new PropertyMetadata("", OnStatusMessageChanged));





        private static void OnStatusMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ////var control = (StatusBoxwithoutinit)d;
            ////string oldValue = (string)e.OldValue;
            ////string newValue = (string)e.NewValue;

            ////control.OnStatusMessageChanged(oldValue, newValue);
        }

        private void OnStatusMessageChanged(string oldValue, string newValue)
        {
            if (newValue.Contains("ERROR:"))
            {
                //MarqueeLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                //MarqueeLabel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                Visibility vb = (Visibility)InitButtonVisbility;
                vb = Visibility.Visible;
            }
            else if (newValue.Contains("WARNING"))
            {
                //MarqueeLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                //MarqueeLabel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);

            }
            else
            {
                //MarqueeLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                //MarqueeLabel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);

            }
        }

        // DependencyProperty for ScrollControl
        public static readonly DependencyProperty ScrollControlProperty =
            DependencyProperty.Register(
                "ScrollControl",
                typeof(bool),
                typeof(StatusUc),
                new PropertyMetadata(false, ScrollControllerChanged));

        public bool ScrollControl
        {
            get { return (bool)GetValue(ScrollControlProperty); }
            set { SetValue(ScrollControlProperty, value); }
        }

        private static void ScrollControllerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (StatusBoxwithoutinit)d;
            //bool newValue = (bool)e.NewValue;

            //if (newValue)
            //{
            //    control.StartMarqueeAnimation();
            //}
            //else
            //{
            //    control.StopMarqueeAnimation();
            //}
        }

        private void StartMarqueeAnimation()
        {
            //if (_marqueeStoryboard != null)
            //{
            //    _marqueeStoryboard.Stop();
            //}

            //double from = MarqueeCanvas.ActualWidth;
            //double to = -MarqueeLabel.ActualWidth;

            //var animation = new DoubleAnimation
            //{
            //    From = from,
            //    To = to,
            //    Duration = new Duration(TimeSpan.FromSeconds(5)),
            //    RepeatBehavior = RepeatBehavior.Forever
            //};

            //Storyboard.SetTarget(animation, MarqueeLabel);
            //Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            //_marqueeStoryboard = new Storyboard();
            //_marqueeStoryboard.Children.Add(animation);
            //_marqueeStoryboard.Begin();
        }

        private void StopMarqueeAnimation()
        {
            //if (_marqueeStoryboard != null)
            //{
            //    _marqueeStoryboard.Stop();
            //    _marqueeStoryboard = null;
            //}
        }
        public StatusUc()
        {
            InitializeComponent();
        }
    }
}
