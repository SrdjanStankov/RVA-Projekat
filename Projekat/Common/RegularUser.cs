using System;
using System.Runtime.Serialization;

namespace Common
{
	[Serializable]
	[DataContract]
	public class RegularUser : User
	{
		public RegularUser()
		{
		}

		public RegularUser(string name, string lastname, string username, string password) : base(name, lastname, username, password)
		{
		}
	}
}
