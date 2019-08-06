using Client.Model;
using Common;
using System.ServiceModel;

namespace Client.ViewModel
{
	public class MainWindowViewModel : BindableBase
	{
		public static InstanceContext connectionContext = new InstanceContext(new ConnectionCallback());

		private BindableBase currentViewModel;
		private BindableBase menuViewModel;

		public Command LoginCommand { get; set; }

		// reference na ostale View Modele
		private LoginViewModel login = new LoginViewModel();
		private DashboardViewModel dashboard = new DashboardViewModel();
		private MenuViewModel menu = new MenuViewModel();
		private AddUserViewModel addUser = new AddUserViewModel();
		private PlannersViewModel planners = new PlannersViewModel();

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
			ChangingViewEvents.Instance.PlannersEvent += (sender, e) => CurrentViewModel = planners;
			ChangingViewEvents.Instance.MenuEvent += (sender, e) => MenuViewModel = menu;
			ChangingViewEvents.Instance.AddUserEvent += (sender, e) => CurrentViewModel = addUser;
			ChangingViewEvents.Instance.LogoutEvent += (sender, e) => { MenuViewModel = null; LoginCommand.Execute(null); };
		}
	}
}
