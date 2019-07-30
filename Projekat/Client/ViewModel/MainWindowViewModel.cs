using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class MainWindowViewModel : BindableBase
	{
		private BindableBase currentViewModel;
		private BindableBase menuViewModel;

		public Command LoginCommand { get; set; }

		// reference na ostale View Modele
		private LoginViewModel login = new LoginViewModel();
		private DashboardViewModel dashboard = new DashboardViewModel();
		private MenuViewModel menu = new MenuViewModel();

		public BindableBase CurrentViewModel
		{
			get => currentViewModel;

			set => SetProperty(ref currentViewModel, value);
		}
		public BindableBase MenuViewModel
		{
			get => menuViewModel;

			set => SetProperty(ref menuViewModel, value);
		}

		public MainWindowViewModel()
		{
			CurrentViewModel = login;
			MenuViewModel = null;

			LoginCommand = new Command(() => CurrentViewModel = login);

			ChangingViewEvents.Instance.DashboardEvent += (sender, e) => CurrentViewModel = dashboard;
			ChangingViewEvents.Instance.MenuEvent += (sender, e) => MenuViewModel = menu;
		}
	}
}
