using DataGridSample.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace DataGridSample
{
    public class MainWindowVM
    {
        /// <summary>
        /// 软件列表
        /// </summary>
        public ObservableCollection<SystemParam> SystemParamList { get; set; }

        public ICommand QueryCommand { get; private set; }

        public MainWindowVM()
        {
            SystemParamList = new ObservableCollection<SystemParam>
            {
                new SystemParam() { ID = 1, Name = "开关测试1", ParamType = SystemParamType.Bool, ParamValue = "True" },
                new SystemParam() { ID = 2, Name = "开关测试2", ParamType = SystemParamType.Bool, ParamValue = "False" },
                new SystemParam() { ID = 3, Name = "数值测试1", ParamType = SystemParamType.Int, ParamValue = "Green" },
                new SystemParam() { ID = 4, Name = "数值测试2", ParamType = SystemParamType.Int, ParamValue = "Red" },
                new SystemParam() { ID = 5, Name = "字符串测试", ParamType = SystemParamType.String, ParamValue = "测试测试测试" }
            };

            QueryCommand = new RelayCommand((o) => 
            {
                foreach (var systemParam in SystemParamList)
                {
                    Console.WriteLine($"{systemParam.ID} {systemParam.Name} {systemParam.ParamType} {systemParam.ParamValue}");
                }
            });
        }
    }
}
