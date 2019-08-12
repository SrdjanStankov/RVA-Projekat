using Client.Model;
using Common;
using log4net;
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
				OnPropertyChanged(nameof(User));
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
			OnPropertyChanged(nameof(User));
		}

		private void OnSave()
		{
			User.Validate();
			if (!User.IsValid)
			{
				if (string.IsNullOrEmpty(User.ValidationErrors["Name"]))
				{
					MessageQueue.Enqueue(User.ValidationErrors["Name"]);
					User.ValidationErrors["Name"] = "*";
				}
				if (string.IsNullOrEmpty(User.ValidationErrors["Lastname"]))
				{
					MessageQueue.Enqueue(User.ValidationErrors["Lastname"]);
					User.ValidationErrors["Lastname"] = "*";
				}
				OnPropertyChanged(nameof(User));
				return;
			}

			try
			{
				LoginViewModel.proxy.ChangeUserData(newUser: User);
				LogManager.GetLogger(typeof(DashboardViewModel)).Info($"Changed data of user: {User.Username}");
				MessageQueue.Enqueue("Changes saved.");
			}
			catch (Exception) { }
		}
	}
}
