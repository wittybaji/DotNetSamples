using GrowlSample.MVVM.Controls;
using System.Windows;

namespace GrowlSample
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

        private void ButtonSuccess_Click(object sender, RoutedEventArgs e)
        {
            Growl.Success("Success");
        }
        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            Growl.Info("Info");
        }
        private void ButtonWarning_Click(object sender, RoutedEventArgs e)
        {
            Growl.Warning("Warning");
        }
        private void ButtonError_Click(object sender, RoutedEventArgs e)
        {
            Growl.Error("Error");
        }
        private void ButtonFatal_Click(object sender, RoutedEventArgs e)
        {
            Growl.Fatal("Fatal");
        }
        private void ButtonAsk_Click(object sender, RoutedEventArgs e)
        {
            Growl.Ask("Ask", isConfirmed => { Growl.Info($"Ask result:{isConfirmed}"); return true; });
        }


        private void ButtonSuccessGlobal_Click(object sender, RoutedEventArgs e)
        {
            Growl.SuccessGlobal("Success");
        }
        private void ButtonInfoGlobal_Click(object sender, RoutedEventArgs e)
        {
            Growl.InfoGlobal("Info");
        }
        private void ButtonWarningGlobal_Click(object sender, RoutedEventArgs e)
        {
            Growl.WarningGlobal("Warning");
        }
        private void ButtonErrorGlobal_Click(object sender, RoutedEventArgs e)
        {
            Growl.ErrorGlobal("Error");
        }
        private void ButtonFatalGlobal_Click(object sender, RoutedEventArgs e)
        {
            Growl.FatalGlobal("Fatal");
        }
        private void ButtonAskGlobal_Click(object sender, RoutedEventArgs e)
        {
            Growl.AskGlobal("Ask", isConfirmed => { Growl.Info($"AskGlobal result:{isConfirmed}"); return true; });
        }
    }
}
