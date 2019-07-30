using System;

namespace Client.Model
{
	public class ChangingViewEvents
	{
		public static ChangingViewEvents Instance { get; set; }

		private ChangingViewEvents() { }

		static ChangingViewEvents()
		{
			Instance = new ChangingViewEvents();
		}

		public event EventHandler DashboardEvent;
		public event EventHandler AddUserEvent;
		public event EventHandler UserLoginSuccessful;
		public event EventHandler MenuEvent;

		public void RaiseMenuEvent()
		{
			MenuEvent?.Invoke(this, EventArgs.Empty);
		}

		public void RaiseDashboardEvent()
		{
			DashboardEvent?.Invoke(this, EventArgs.Empty);
		}

		public void RaiseAddUserEvent()
		{
			AddUserEvent?.Invoke(this, EventArgs.Empty);
		}

		public void RaiseUserLoginSuccessful()
		{
			UserLoginSuccessful?.Invoke(this, EventArgs.Empty);
		}
	}
}
