using Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
	public class Connection : IConnection
	{
		private static List<IConnectionCallback> callbackList = new List<IConnectionCallback>();
		private int registeredUsers;

		public Connection()
		{
		}

		public int Login(string userName, string password)
		{
			var registeredUser = OperationContext.Current.GetCallbackChannel<IConnectionCallback>();

			if (!callbackList.Contains(registeredUser))
			{
				callbackList.Add(registeredUser);
			}

			callbackList.ForEach(
				delegate (IConnectionCallback callback)
				{
					callback.NotifyLogin(userName);
					registeredUsers++;
				});

			Console.WriteLine($"login");
			return registeredUsers;
		}

		public void Change(string userName, string password)
		{
			callbackList.ForEach(
				delegate (IConnectionCallback callback)
				{ callback.NotifyChange(userName); });
		}

		public int Logout(string userName)
		{
			var registeredUser = OperationContext.Current.GetCallbackChannel<IConnectionCallback>();

			if (callbackList.Contains(registeredUser))
			{
				callbackList.Remove(registeredUser);
				registeredUsers--;
			}

			// Notify everyone that user has arrived.
			// Use an anonymous delegate and generics to do our dirty work.
			callbackList.ForEach(
				delegate (IConnectionCallback callback)
				{ callback.NotifyLogout(userName); });

			Console.WriteLine("logout");
			return registeredUsers;
		}
	}
}
