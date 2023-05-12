using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CheckComboBoxSample.MVVM.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 安全执行
        /// </summary>
        /// <param name="action"></param>
        protected void SafelyInvoke(Action action)
        {
            try { action?.Invoke(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        /// <summary>
        /// 安全执行 带一个参
        /// </summary>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        protected void SafelyInvoke<T>(Action<T> action, T parameter)
        {
            try { action?.Invoke(parameter); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        /// <summary>
        /// 安全执行 带两个参
        /// </summary>
        /// <param name="action"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        protected void SafelyInvoke<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            try { action?.Invoke(param1, param2); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
