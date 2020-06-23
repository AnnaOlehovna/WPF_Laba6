using System.Windows;

namespace Laba6.Utils
{
    class Wrapper : DependencyObject
    {

        public static readonly DependencyProperty LowLimitProperty =
             DependencyProperty.Register("lowLimit", typeof(double),
             typeof(Wrapper), new FrameworkPropertyMetadata(null));

        public double lowLimit
        {
            get { return (double)GetValue(LowLimitProperty); }
            set { SetValue(LowLimitProperty, value); }
        }

    }
}
