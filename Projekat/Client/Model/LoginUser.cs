using Common;

namespace Client.Model
{
	public class LoginUser : ValidationBase
	{
		private string username;
		private string password;

		public string Username
		{
			get => username; set
			{
				username = value;
				OnPropertyChanged("Username");
			}
		}

		public string Password
		{
			get => password; set
			{
				password = value;
				OnPropertyChanged("Password");
			}
		}

		protected override void ValidateSelf()
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				ValidationErrors["Username"] = "Required";
			}

			if (string.IsNullOrWhiteSpace(password))
			{
				ValidationErrors["Password"] = "Required";
			}
		}
	}
}
