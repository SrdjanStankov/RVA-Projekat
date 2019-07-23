using System;
using System.Collections.Generic;

namespace Common
{
	[Serializable]
	public class User
	{
		public User(string name, string lastname, string username)
		{
			Name = name;
			Lastname = lastname;
			Username = username;
		}

		public string Name { get; set; }

		public string Lastname { get; set; }

		public string Username { get; set; }
	}
}
