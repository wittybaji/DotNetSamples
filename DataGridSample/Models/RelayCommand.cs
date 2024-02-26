using System;
using System.Windows.Input;

namespace DataGridSample.Models
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object> _excute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> excute, Func<object, bool> canExecute = null)
        {
            _excute = excute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            SafelyInvoke(_excute, parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// 安全执行
        /// </summary>
        /// <param name="action"></param>
        public void SafelyInvoke(Action<object> action, object parameter)
        {
            try { action.Invoke(parameter); }
            catch (Exception ex) { Console.WriteLine($"SafelyInvokeActionException{Environment.NewLine}{ex.Message}"); }
        }
    }
}
