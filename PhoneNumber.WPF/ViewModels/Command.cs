using System;
using System.Windows.Input;

namespace PhoneNumber.WPF.ViewModels
{
    internal class Command : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public Command(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute == null ? () => true : canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute.Invoke();

        public void Execute(object parameter) => _action();
    }
}