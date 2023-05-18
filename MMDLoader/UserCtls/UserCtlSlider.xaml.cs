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

namespace MMDLoader.UserCtls
{
    /// <summary>
    /// UserCtlSlider.xaml 的交互逻辑
    /// </summary>
    public partial class UserCtlSlider : UserControl
    {
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(UserCtlSlider), new PropertyMetadata((double)0));


        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(UserCtlSlider), new PropertyMetadata((double)100));



        public double SmallChange
        {
            get { return (double)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SmallChange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register("SmallChange", typeof(double), typeof(UserCtlSlider), new PropertyMetadata((double)1));



        public double LargeChange
        {
            get { return (double)GetValue(LargeChangeProperty); }
            set { SetValue(LargeChangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LargeChange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeChangeProperty =
            DependencyProperty.Register("LargeChange", typeof(double), typeof(UserCtlSlider), new PropertyMetadata((double)10));

        public double DefaultValue
        {
            get { return (double)GetValue(DefaultValueProperty); }
            set { SetValue(DefaultValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.Register("DefaultValue", typeof(double), typeof(UserCtlSlider), new PropertyMetadata((double)50));



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(UserCtlSlider), new PropertyMetadata((double)0));


        public static readonly RoutedEvent ValueChangedEvent =
           EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserCtlSlider));
        
        public event RoutedEventHandler ValueChanged
        {
            add
            {
                AddHandler(ValueChangedEvent, value);
            }

            remove
            {
                RemoveHandler(ValueChangedEvent, value);
            }
        }
        public UserCtlSlider()
        {
            InitializeComponent();
        }

        private void buttonDefault_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value = DefaultValue;
        }

        private void buttonSubSmall_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value -= SmallChange;
        }

        private void buttonAddSmall_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value += SmallChange;
        }

        private void buttonSubLarge_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value -= LargeChange;
        }

        private void buttonAddLarge_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value += LargeChange;
        }

        private void textBoxValue_LostFocus(object sender, RoutedEventArgs e)
        {
            double value = 0;
            if (double.TryParse(textBoxValue.Text, out value))
                sliderValue.Value = value;
        }

        private void sliderValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RoutedEventArgs argsValueChanged = new RoutedEventArgs(ValueChangedEvent, this);
            RaiseEvent(argsValueChanged);
        }

        private void buttonMin_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value = MinValue;
        }

        private void buttonMax_Click(object sender, RoutedEventArgs e)
        {
            sliderValue.Value = MaxValue;
        }

        public double CurValue { get => sliderValue.Value; }

        private void textBoxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            double value = 0;
            if (double.TryParse(textBoxValue.Text, out value))
            {
                if (value >= MinValue && value <= MaxValue)
                    sliderValue.Value = value;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            buttonAddSmall.Content = string.Format("{0:'+'#.##}",sliderValue.SmallChange);
            buttonSubSmall.Content = string.Format("{0:'-'#.##}", sliderValue.SmallChange);
            buttonAddLarge.Content = string.Format("{0:'+'#.##}", sliderValue.LargeChange);
            buttonSubLarge.Content = string.Format("{0:'-'#.##}", sliderValue.LargeChange);
        }
    }
}
