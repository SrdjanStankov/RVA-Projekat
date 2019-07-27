using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class DashboardViewModel : BindableBase
	{
		public Command<object> SaveCommand { get; set; }

		private User user;

		public User User
		{
			get => user; set
			{
				user = value;
				OnPropertyChanged("User");
			}
		}

		public DashboardViewModel()
		{
			SaveCommand = new Command<object>(OnSave);


			//User = LoginViewModel.proxy.GetUser(LoginViewModel.factory.Credentials.UserName.UserName);
		}

		private void OnSave(object obj)
		{
			User.Password = (obj as System.Windows.Controls.PasswordBox).Password;
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
