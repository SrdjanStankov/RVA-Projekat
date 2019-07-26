using System;

namespace Common
{
	[Serializable]
	public class RegularUser : User
	{
		public RegularUser()
		{
		}

		public RegularUser(string name, string lastname, string username, string password, Planner planner) : base(name, lastname, username, password, planner)
		{
		}
	}
}
