using System;
using System.Windows.Input;

namespace takojsnje_sporocanje{
    public class Command : ICommand{
        public Command(Action<object?> action) => this.action = action;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) {
            action?.Invoke(parameter);
        }

        private Action<object?> action;
    }
}