using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimationTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double from = 300;
            double to = -300;
            double duration = 4 * 1000;
            var transform = new TranslateTransform
            {
                X = from
            };
            BtnTest.RenderTransform = transform;
            var animation = new DoubleAnimation(to, new Duration(TimeSpan.FromMilliseconds(duration)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
    }
}
