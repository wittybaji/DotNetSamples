using CheckComboBoxSample.MVVM.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CheckComboBoxSample.MVVM.ViewModels
{
    internal class MainWindowVM : ObservableObject
    {
        public ObservableCollection<string> StationList { get; set; }
        public ObservableCollection<string> SelectedStation { get; set; }

        public ICommand QueryCommand { get; }

        public MainWindowVM()
        {
            SelectedStation = new ObservableCollection<string>();
            StationList = new ObservableCollection<string>()
            {
                "牡丹园",
                "北太平庄",
                "积水潭",
                "平安里",
                "太平桥",
                "牛街",
                "景风门",
                "草桥",
                "新发地",
                "新宫",

            };
            QueryCommand = new RelayCommand((o) =>
            {
                foreach (var station in SelectedStation)
                {
                    System.Console.WriteLine(station);
                }
            });
        }
    }
}
