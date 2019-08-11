using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class DashboardViewModel : BindableBase
	{
		public Command SaveCommand { get; set; }

		private User user;

		public User User
		{
			get => user;
			set
			{
				user = value;
				OnPropertyChanged("User");
			}
		}

		public DashboardViewModel()
		{
			SaveCommand = new Command(OnSave);
			ChangingViewEvents.Instance.UserLoginSuccessful += SetupUser;
		}

		private void SetupUser(object sender, System.EventArgs e)
		{
			string username = LoginViewModel.factory.Credentials.UserName.UserName;
			User = LoginViewModel.proxy.GetUser(username);
			OnPropertyChanged("User");
		}

		private void OnSave()
		{
			User.Validate();
			if (!User.IsValid)
			{
				return;
			}

			try
			{
				LoginViewModel.proxy.ChangeUserData(newUser: User);
			}
			catch (System.Exception)
			{

			}
		}
	}
}
