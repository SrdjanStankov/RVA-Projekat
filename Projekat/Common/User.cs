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
				OnPropertyChanged(nameof(Name));
			}
		}

		[DataMember]
		public string Lastname
		{
			get => lastname; set
			{
				lastname = value;
				OnPropertyChanged(nameof(Lastname));
			}
		}

		[Key]
		[DataMember]
		public string Username
		{
			get => username; set
			{
				username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

		[DataMember]
		public string Password
		{
			get => password; set
			{
				password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		#endregion

		protected override void ValidateSelf()
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
			{
				ValidationErrors[nameof(Name)] = "Name is required";
			}

			if (string.IsNullOrWhiteSpace(lastname) || string.IsNullOrEmpty(lastname))
			{
				ValidationErrors[nameof(Lastname)] = "Lastname is required";
			}

			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
			{
				ValidationErrors[nameof(Username)] = "Username is required";
			}

			if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
			{
				ValidationErrors[nameof(Password)] = "Password is required";
			}
		}
	}
}
