using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Lab6
{
    //команда
    class Command : ICommand
    {
        //делегат, выполняющий действие команды
        private Action<object> execute;
        //делегат предикат, определяет, возможно ли выполнение команды
        private Func<bool> canExecute;

        //событие, вызывается когда canExecute может измениться
        public event EventHandler CanExecuteChanged
        {          
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object _)
        {
            return this.canExecute == null || this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
