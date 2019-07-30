using Client.Model;
using Common;
using System.Windows.Controls;

namespace Client.ViewModel
{
	public class AddUserViewModel : BindableBase
	{
		private User user;

		public Command<object> CreateUserCommand { get; set; }
		public string SelectedRole { get; set; }
		public User User
		{
			get => user;
			set
			{
				user = value;
				OnPropertyChanged("User");
			}
		}

		public AddUserViewModel()
		{
			CreateUserCommand = new Command<object>(OnCreate);
		}

		private void OnCreate(object obj)
		{
			User.Password = (obj as PasswordBox).Password;
			User.Validate();

			if (!User.IsValid)
			{
				return;
			}

			switch (SelectedRole)
			{
				case "Administrator":

					break;
				default:
					break;
			}
		}
	}
}
