using Common;
using System;

namespace Server
{
	public class Connection : IConnection
	{
		public void Login(User user)
		{
			Console.WriteLine($"Login");
		}

		public void Logout(User user)
		{
			Console.WriteLine($"Logout");
		}
	}
}
