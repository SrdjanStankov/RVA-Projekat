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

		public Connection()
		{
		}

		public void Login(string userName, string password)
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

			Console.WriteLine($"Login: {userName}");
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

		public void Logout(string userName)
		{
			if (callbackList.ContainsKey(userName/* registeredUser*/))
			{
				callbackList.Remove(userName/*registeredUser*/);
			}

			//callbackList.ForEach(
			//	delegate (IConnectionCallback callback)
			//	{ callback.NotifyLogout(userName); });

			Console.WriteLine($"Logout: {userName}");
		}

		public void ChangeUserData(User newUser)
		{
			Console.WriteLine($"Changing user data: {newUser.Username}");
			using (var ctx = new ModelContext())
			{
				ctx.ChangeUser(newUser);
			}
		}

		public User GetUser(string username)
		{
			Console.WriteLine($"Get user: {username}");
			using (var ctx = new ModelContext())
			{
				var u = ctx.GetUser(username);
				if (u is Administrator)
				{
					var administrator = new Administrator(u.Name, u.Lastname, u.Username, u.Password);
					return administrator;
				}
				if (u is RegularUser)
				{
					var regularUser = new RegularUser(u.Name, u.Lastname, u.Username, u.Password);
					return regularUser;
				}
			}

			return null;
		}

		public bool AddUser(User newUser)
		{
			Console.WriteLine($"Adding user: {newUser.Username}");
			using (var ctx = new ModelContext())
			{
				if (!ctx.ExistUser(newUser.Username))
				{
					ctx.AddUser(newUser);
					return true;
				}
			}
			return false;
		}

		public void AddPlanner(Planner planner)
		{
			Console.WriteLine($"Adding planner:{planner.Name}");
			using (var ctx = new ModelContext())
			{
				ctx.AddPlanner(planner);
			}
		}

		public List<Planner> GetPlanners()
		{
			Console.WriteLine($"Getting planners");
			using (var ctx = new ModelContext())
			{
				return ctx.GetPlanners();
			}
		}
	}
}
