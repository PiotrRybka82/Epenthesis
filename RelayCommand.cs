using System;
using System.Windows.Input;

namespace Epenthesis_2.ViewModel
{
    class RelayCommand : ICommand
    {

        readonly Action<object> _method;
        readonly Predicate<object> _condition;



        public RelayCommand(Action<object> method)
            : this(method, null)
        {
        }

        public RelayCommand(Action<object> method, Predicate<object> condition)
        {
            this._condition = condition;
            this._method = method;
        }





        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this._condition == null ? true : this._condition(parameter);
        }

        public void Execute(object parameter)
        {
            this._method(parameter);
        }

        public void Execute()
        {
            this._method(null);
        }


    }
}
