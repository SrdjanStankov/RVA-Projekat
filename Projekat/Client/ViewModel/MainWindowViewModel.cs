using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class MainWindowViewModel : BindableBase
	{
		//private static MainWindowViewModel instance = null;

		private BindableBase currentViewModel;

		public Command LoginCommand { get; set; }

		// reference na ostale View Modele
		private LoginViewModel login = new LoginViewModel();
		private DashboardViewModel dashboard = new DashboardViewModel();

		public BindableBase CurrentViewModel
		{
			get => currentViewModel;

			set => SetProperty(ref currentViewModel, value);
		}
		//public static MainWindowViewModel Instance
		//{
		//	get
		//	{
		//		if (instance is null)
		//		{
		//			instance = new MainWindowViewModel();
		//		}
		//		return instance;
		//	}
		//	set => instance = value;
		//}

		public MainWindowViewModel()
		{
			CurrentViewModel = login;

			LoginCommand = new Command(() => CurrentViewModel = login);
		}

		public void ShowDashboard()
		{
			CurrentViewModel = dashboard;
		}
	}
}
