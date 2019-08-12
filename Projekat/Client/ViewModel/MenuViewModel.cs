using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class MenuViewModel : BindableBase
	{
		private object isAdmin;

		public Command DashboardCommand { get; set; }
		public Command LogoutCommand { get; set; }
		public Command AddUserCommand { get; set; }
		public Command PlannersCommand { get; set; }
		public Command LogEventsCommand { get; set; }

		public object IsAdmin
		{
			get => LoginViewModel.proxy.GetUser(LoginViewModel.factory.Credentials.UserName.UserName) is Administrator ? new object() : null;

			set => isAdmin = value;
		}

		public MenuViewModel()
		{
			DashboardCommand = new Command(() => ChangingViewEvents.Instance.RaiseDashboardEvent());
			LogoutCommand = new Command(() => ChangingViewEvents.Instance.RaiseLogoutEvent());
			AddUserCommand = new Command(() => ChangingViewEvents.Instance.RaiseAddUserEvent());
			PlannersCommand = new Command(() => ChangingViewEvents.Instance.RaisePlannersEvent());
			LogEventsCommand = new Command(() => ChangingViewEvents.Instance.RaiseLogtEvent());
		}
	}
}
