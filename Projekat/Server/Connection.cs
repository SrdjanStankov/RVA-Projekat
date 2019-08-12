using Common;
using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
	public class Connection : IConnection
	{
		private static Dictionary<string, IConnectionCallback> callbackList = new Dictionary<string, IConnectionCallback>();
		private ILog log = LogManager.GetLogger(typeof(Connection));

		public Connection()
		{
		}

		public void Login(string userName, string password)
		{
			var registeredUser = OperationContext.Current.GetCallbackChannel<IConnectionCallback>();

			if (!callbackList.ContainsKey(userName))
			{
				callbackList.Add(userName, registeredUser);
			}

			Console.WriteLine($"Login: {userName}");
			log.Info($"User {userName}|{password} logged in");
		}

		public void Change(string userName)
		{
			foreach (var item in callbackList)
			{
				if (item.Key != userName)
				{
					item.Value.NotifyChange();
				}
			}

			Console.WriteLine($"Change");
			log.Debug($"Notified all users for changes");
		}

		public void Logout(string userName)
		{
			if (callbackList.ContainsKey(userName))
			{
				callbackList.Remove(userName);
			}

			Console.WriteLine($"Logout: {userName}");
			log.Info($"User {userName} logged out");
		}

		public void ChangeUserData(User newUser)
		{
			Console.WriteLine($"Changing user data: {newUser.Username}");
			log.Debug($"Editing user: {newUser.Username}");
			using (var ctx = new ModelContext())
			{
				ctx.EditUser(newUser);
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

		public void AddPlanner(Planner planner, string usernameThatAdded)
		{
			Console.WriteLine($"Adding planner:{planner.Name}");
			using (var ctx = new ModelContext())
			{
				ctx.AddPlanner(planner);
			}
			Change(usernameThatAdded);
		}

		public List<Planner> GetPlanners()
		{
			Console.WriteLine($"Getting planners");
			using (var ctx = new ModelContext())
			{
				return ctx.GetPlanners();
			}
		}

		public void AddEvent(Event @event, int plannerId, string usernameThatAdded)
		{
			Console.WriteLine($"Adding event to planner {plannerId}");
			using (var ctx = new ModelContext())
			{
				ctx.AddEvent(@event, plannerId);
			}
			Change(usernameThatAdded);
		}

		public void RemovePlanner(int id, string usernameThatAdded)
		{
			Console.WriteLine($"Removing planner: {id}");
			using (var ctx = new ModelContext())
			{
				ctx.RemovePlanner(id);
			}
			Change(usernameThatAdded);
		}

		public Planner GetPlanner(int id)
		{
			Console.WriteLine($"Getting planner: {id}");
			using (var ctx = new ModelContext())
			{
				return ctx.GetPlanner(id);
			}
		}

		public void EditPlanner(Planner planner, string usernameThatAdded)
		{
			Console.WriteLine($"Editing planner: {planner.Id}");
			using (var ctx = new ModelContext())
			{
				ctx.EditPlanner(planner);
			}
			Change(usernameThatAdded);
		}

		public void EditEvent(Event @event, string userName)
		{
			Console.WriteLine($"Editing event: {@event.Id}");
			using (var ctx = new ModelContext())
			{
				ctx.EditEvent(@event);
			}
			Change(userName);
		}

		public Event GetEvent(int id)
		{
			Console.WriteLine($"Getting event: {id}");
			using (var ctx = new ModelContext())
			{
				return ctx.GetEvent(id);
			}
		}

		public void RemoveEvent(int id, string usernameThatAdded)
		{
			Console.WriteLine($"Removing event: {id}");
			using (var ctx = new ModelContext())
			{
				ctx.RemoveEvent(id);
			}
			Change(usernameThatAdded);
		}

		public List<Event> GetEvents()
		{
			Console.WriteLine($"Getting events");
			using (var ctx = new ModelContext())
			{
				return ctx.GetEvents();
			}
		}
	}
}
