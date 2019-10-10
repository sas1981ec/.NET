using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Proasoft.InfraestructuraVM
{
    internal class RelayCommand<T> : ICommand
    {
        #region Declarations
        readonly Predicate<T> _canExecute;
        readonly Action<T> _execute;
        #endregion

        #region Constructors
        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(Object parameter)
        {
            _execute((T)parameter);
        }
        #endregion
    }

    internal class RelayCommand : ICommand
    {
        #region Declarations
        readonly Func<Boolean> _canExecute;
        readonly Action _execute;
        #endregion

        #region Constructors
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<Boolean> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(Object parameter)
        {
            _execute();
        }
        #endregion
    }
}
