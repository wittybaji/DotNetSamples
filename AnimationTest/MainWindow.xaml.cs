using System;
using System.Windows;
using System.Windows.Controls;
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
            var panel = TipsPanel;
            TextBlock tips = new TextBlock()
            {
                Width = 200,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Background = new SolidColorBrush(Colors.Gray),
                FontSize = 22,
                Foreground = new SolidColorBrush(Colors.White),
                Text = $"通知内容 {DateTime.Now:HH:mm:ss}",
                TextAlignment = TextAlignment.Center
            };

            double from = ActualWidth / 2 + tips.Width / 2;
            double to = -from;
            double duration = 6 * 1000;
            var transform = new TranslateTransform { X = from };
            tips.RenderTransform = transform;
            var animation = new DoubleAnimation(to, new Duration(TimeSpan.FromMilliseconds(duration)));
            animation.Completed += (cs, ce) =>
            {
                panel.Children.Remove(tips);
            };
            panel.Children.Add(tips);
            transform.BeginAnimation(TranslateTransform.XProperty, animation);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var panel = FontColorPanel;
            NameScope.SetNameScope(this, new NameScope());
            TextBlock tips = new TextBlock()
            {
                Width = 200,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Background = new SolidColorBrush(Colors.Gray),
                FontSize = 22,
                Text = $"通知内容 {DateTime.Now:HH:mm:ss}",
                TextAlignment = TextAlignment.Center
            };
            var foreground = new LinearGradientBrush(Color.FromRgb(0xFF, 0, 0), Color.FromRgb(0, 0, 0xFF), new Point(0, 0), new Point(1, 0));
            GradientStop stop1 = new GradientStop(Colors.Red, 0.0);
            GradientStop stop2 = new GradientStop(Colors.Blue, 0.0);
            RegisterName("GradientStop1", stop1);
            RegisterName("GradientStop2", stop2);
            foreground.GradientStops.Add(stop1);
            foreground.GradientStops.Add(stop2);

            tips.Foreground = foreground;

            var offsetAnimation = new DoubleAnimation(1, new Duration(TimeSpan.FromMilliseconds(5000)));
            Storyboard.SetTargetName(offsetAnimation, "GradientStop1");
            Storyboard.SetTargetName(offsetAnimation, "GradientStop2");
            Storyboard.SetTargetProperty(offsetAnimation, new PropertyPath(GradientStop.OffsetProperty));
            Storyboard gradientStopAnimationStoryboard = new Storyboard();
            gradientStopAnimationStoryboard.Children.Add(offsetAnimation);

            tips.Loaded += (s1, e1) => { gradientStopAnimationStoryboard.Begin(); };
            panel.Children.Add(tips);
        }
    }
}
