using Client.Model;
using Common;
using MaterialDesignThemes.Wpf;
using System;

namespace Client.ViewModel
{
	public class DashboardViewModel : BindableBase
	{
		public Command SaveCommand { get; set; }
		public SnackbarMessageQueue MessageQueue { get; set; }

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
			MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
		}

		private void SetupUser(object sender, EventArgs e)
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
				if (User.ValidationErrors["Name"] != "")
				{
					MessageQueue.Enqueue(User.ValidationErrors["Name"]);
					User.ValidationErrors["Name"] = "*";
				}
				if (User.ValidationErrors["Lastname"] != "")
				{
					MessageQueue.Enqueue(User.ValidationErrors["Lastname"]);
					User.ValidationErrors["Lastname"] = "*";
				}
				OnPropertyChanged("User");
				return;
			}

			try
			{
				LoginViewModel.proxy.ChangeUserData(newUser: User);
				MessageQueue.Enqueue("Changes saved.");
			}
			catch (Exception) { }
		}
	}
}
