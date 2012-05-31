using System;
using System.Windows.Input;

namespace ServerInventoryPiao.Controllers
{
    public class RelayCommand : ICommand
    {
        public Action _action;
        private Func<bool> _canAction;
        
        public RelayCommand(Action action) : this(action, null) { }

        public RelayCommand(Action action, Func<bool> canAction)
        {
            _action = action;
            _canAction = canAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canAction == null ? true : _canAction();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
            }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public Action<T> _action;
        private Func<T, bool> _canAction;

        public RelayCommand(Action<T> action) : this(action, null) { }

        public RelayCommand(Action<T> action, Func<T, bool> canAction)
        {
            _action = action;
            _canAction = canAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canAction == null ? true : _canAction((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action((T)parameter);
            }
        }
    }
}
