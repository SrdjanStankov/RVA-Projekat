using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class MainWindowViewModel : BindableBase
	{
		private BindableBase currentViewModel;

		public Command LoginCommand { get; set; }

		// reference na ostale View Modele
		private LoginViewModel login = new LoginViewModel();

		public BindableBase CurrentViewModel
		{
			get => currentViewModel;

			set => SetProperty(ref currentViewModel, value);
		}

		public MainWindowViewModel()
		{
			CurrentViewModel = login;

			LoginCommand = new Command(() => CurrentViewModel = login);
		}

	}
}
