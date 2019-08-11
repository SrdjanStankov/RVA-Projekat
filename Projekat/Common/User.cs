using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Common
{
	[Serializable]
	[DataContract]
	[KnownType(typeof(RegularUser))]
	[KnownType(typeof(Administrator))]
	public abstract class User : ValidationBase
	{
		private string name;
		private string lastname;
		private string username;
		private string password;

		public User(string name, string lastname, string username, string password)
		{
			Name = name;
			Lastname = lastname;
			Username = username;
			Password = password;
		}

		protected User() { }

		#region Properties

		[DataMember]
		public string Name
		{
			get => name; set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		[DataMember]
		public string Lastname
		{
			get => lastname; set
			{
				lastname = value;
				OnPropertyChanged("Lastname");
			}
		}

		[Key]
		[DataMember]
		public string Username
		{
			get => username; set
			{
				username = value;
				OnPropertyChanged("Username");
			}
		}

		[DataMember]
		public string Password
		{
			get => password; set
			{
				password = value;
				OnPropertyChanged("Password");
			}
		}

		#endregion

		protected override void ValidateSelf()
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
			{
				ValidationErrors["Name"] = "Name is required";
			}

			if (string.IsNullOrWhiteSpace(lastname) || string.IsNullOrEmpty(lastname))
			{
				ValidationErrors["Lastname"] = "Lastname is required";
			}

			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
			{
				ValidationErrors["Username"] = "Username is required";
			}

			if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
			{
				ValidationErrors["Password"] = "Password is required";
			}
		}
	}
}
