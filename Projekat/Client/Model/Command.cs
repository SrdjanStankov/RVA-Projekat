using System;
using System.Windows.Input;

namespace Client.Model
{
	public class Command : ICommand
	{
		public event EventHandler CanExecuteChanged = delegate { };

		private Action _TargetExecuteMethod;
		private Func<bool> _TargetCanExecuteMethod;

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
			return _TargetCanExecuteMethod != null ? _TargetCanExecuteMethod() : _TargetExecuteMethod != null;
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
		private Action<T> _TargetExecuteMethod;
		private Func<T, bool> _TargetCanExecuteMethod;

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
				var tparm = (T)parameter;
				return _TargetCanExecuteMethod(tparm);
			}

			return _TargetExecuteMethod != null;
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
