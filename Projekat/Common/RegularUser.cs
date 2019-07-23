using System;

namespace Common
{
	[Serializable]
	public class RegularUser : User
	{
		public RegularUser(string name, string lastname, string username) : base(name, lastname, username)
		{
		}
	}
}
