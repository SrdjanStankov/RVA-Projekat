using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Common
{
	[Serializable]
	[KnownType(typeof(RegularUser))]
	[KnownType(typeof(Administrator))]
	public abstract class User : ValidationBase
	{
		private string name;
		private string lastname;
		private string username;
		private string password;
		private Planner planner;

		public User(string name, string lastname, string username, string password, Planner planner)
		{
			Name = name;
			Lastname = lastname;
			Username = username;
			Password = password;
			Planner = planner;
		}

		protected User()
		{
		}

		#region Properties

		public string Name
		{
			get => name; set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		public string Lastname
		{
			get => lastname; set
			{
				lastname = value;
				OnPropertyChanged("Lastname");
			}
		}

		[Key]
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

		public virtual Planner Planner
		{
			get => planner; set
			{
				planner = value;
				OnPropertyChanged("Planner");
			}
		}

		#endregion

		protected override void ValidateSelf()
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				ValidationErrors["Name"] = "Required";
			}

			if (string.IsNullOrWhiteSpace(lastname))
			{
				ValidationErrors["Lastname"] = "Required";
			}

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
