using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class MenuViewModel : BindableBase
	{
		public Command DashboardCommand { get; set; }

		public Command LogoutCommand { get; set; }

		public MenuViewModel()
		{
			DashboardCommand = new Command(() => System.Console.WriteLine());
			LogoutCommand = new Command(() => System.Console.WriteLine());
		}
	}
}
