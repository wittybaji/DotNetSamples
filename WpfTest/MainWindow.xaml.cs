using System.Windows.Media;

namespace WpfTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new LinearGradientBrush(Color.FromRgb(1, 1, 1), Color.FromRgb(1, 1, 1), 45);
        }


    }
}
