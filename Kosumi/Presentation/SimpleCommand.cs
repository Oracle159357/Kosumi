using System;
using System.Windows.Input;

namespace Kosumi.Presentation
{
    public class SimpleCommand : ICommand
    {
        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public SimpleCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public SimpleCommand(Action execute, Func<bool> canExecute = null) :
            this(o => execute(), o => canExecute?.Invoke() ?? true)
        {
        }

        public SimpleCommand() : this(() => { })
        {
        }

        public Action<object> ExecuteDelegate
        {
            set { _execute = value ?? (o => { }); }
            get => _execute;
        }

        public Func<object, bool> CanExecuteDelegate
        {
            set { _canExecute = value ?? (o => true); }
            get => _canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}