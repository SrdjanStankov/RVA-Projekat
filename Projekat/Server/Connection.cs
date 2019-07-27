using Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
	public class Connection : IConnection
	{
		private static Dictionary<string, IConnectionCallback> callbackList = new Dictionary<string, IConnectionCallback>();
#pragma warning disable 649
		private int registeredUsers;
#pragma warning restore 649

		public Connection()
		{
		}

		public int Login(string userName, string password)
		{
			var registeredUser = OperationContext.Current.GetCallbackChannel<IConnectionCallback>();

			if (!callbackList.ContainsKey(userName/*registeredUser*/))
			{
				callbackList.Add(userName, registeredUser);
			}

			//callbackList[userName].NotifyLogin(userName);
			
			//callbackList.ForEach(
			//	delegate (IConnectionCallback callback)
			//	{
			//		callback.NotifyLogin(userName);
			//		registeredUsers++;
			//	});

			Console.WriteLine($"login");
			return registeredUsers;
		}

		public void Change(string userName, string password)
		{
			callbackList[userName].NotifyChange(userName);

			foreach (var item in callbackList)
			{
				item.Value.NotifyChange(userName);
			}

			Console.WriteLine($"Change");

			//callbackList.ForEach(
			//	delegate (IConnectionCallback callback)
			//	{ callback.NotifyChange(userName); });
		}

		public int Logout(string userName)
		{
			var registeredUser = OperationContext.Current.GetCallbackChannel<IConnectionCallback>();

			if (callbackList.ContainsKey(userName/* registeredUser*/))
			{
				callbackList.Remove(userName/*registeredUser*/);
			}

			//callbackList.ForEach(
			//	delegate (IConnectionCallback callback)
			//	{ callback.NotifyLogout(userName); });

			Console.WriteLine("logout");
			return registeredUsers;
		}

		public void ChangeUserData(User newUser)
		{
			Console.WriteLine("Changing User Data");
		}

		public User GetUser(string userName)
		{
			using (var ctx = new ModelContext())
			{
				return ctx.GetUser(userName);
			}
		}
	}
}
