using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class MenuViewModel : BindableBase
	{
		public Command DashboardCommand { get; set; }
		public Command LogoutCommand { get; set; }
		public Command AddUserCommand { get; set; }


		public MenuViewModel()
		{
			DashboardCommand = new Command(() => ChangingViewEvents.Instance.RaiseDashboardEvent());
			LogoutCommand = new Command(() => ChangingViewEvents.Instance.RaiseLogoutEvent());
			AddUserCommand = new Command(() => ChangingViewEvents.Instance.RaiseAddUserEvent());
		}
	}
}
