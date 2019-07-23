using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Common
{
	[Serializable]
	[KnownType(typeof(RegularUser))]
	[KnownType(typeof(Administrator))]
	public abstract class User
	{
		public User(string name, string lastname, string username)
		{
			Name = name;
			Lastname = lastname;
			Username = username;
		}

		public string Name { get; set; }

		public string Lastname { get; set; }

		[Key]
		public string Username { get; set; }

		public virtual Planner Planner { get; set; }
	}
}
