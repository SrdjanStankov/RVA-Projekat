using System;
using System.Windows.Input;

namespace Client.Model
{
	public class Command : ICommand
	{
		public event EventHandler CanExecuteChanged = delegate { };

		Action _TargetExecuteMethod;
		Func<bool> _TargetCanExecuteMethod;

		public Command(Action executeMethod)
		{
			_TargetExecuteMethod = executeMethod;
		}

		public Command(Action executeMethod, Func<bool> canExecuteMethod)
		{
			_TargetExecuteMethod = executeMethod;
			_TargetCanExecuteMethod = canExecuteMethod;
		}


		public bool CanExecute(object parameter)
		{
			if (_TargetCanExecuteMethod != null)
			{
				return _TargetCanExecuteMethod();
			}

			if (_TargetExecuteMethod != null)
			{
				return true;
			}

			return false;
		}

		public void Execute(object parameter)
		{
			if (_TargetExecuteMethod != null)
			{
				_TargetExecuteMethod();
			}
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged(this, EventArgs.Empty);
		}
	}

	public class Command<T> : ICommand
	{
		Action<T> _TargetExecuteMethod;
		Func<T, bool> _TargetCanExecuteMethod;

		public Command(Action<T> executeMethod)
		{
			_TargetExecuteMethod = executeMethod;
		}

		public Command(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
		{
			_TargetExecuteMethod = executeMethod;
			_TargetCanExecuteMethod = canExecuteMethod;
		}

		public event EventHandler CanExecuteChanged = delegate { };

		public bool CanExecute(object parameter)
		{
			if (_TargetCanExecuteMethod != null)
			{
				T tparm = (T)parameter;
				return _TargetCanExecuteMethod(tparm);
			}

			if (_TargetExecuteMethod != null)
			{
				return true;
			}

			return false;
		}

		public void Execute(object parameter)
		{
			_TargetExecuteMethod?.Invoke((T)parameter);
		}
		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged(this, EventArgs.Empty);
		}
	}
}
