using Common;
using System;
using System.ServiceModel;

namespace Server
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	public class Connection : IConnection
	{
		public void Login(string userName, string password)
		{
			Console.WriteLine($"login");
		}

		public void Logout(string userName)
		{
			Console.WriteLine("logout");
		}

	}
}
