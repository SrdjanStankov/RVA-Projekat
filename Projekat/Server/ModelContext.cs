using Common;
using log4net;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Server
{
	public class ModelContext : DbContext
	{
		private ILog log = LogManager.GetLogger(typeof(ModelContext));

		public DbSet<Planner> Planners { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Event> Events { get; set; }

		public ModelContext() : base("name=ModelContext")
		{
			Database.CreateIfNotExists();
			AddAdminIfNotExist();
		}

		private void AddAdminIfNotExist()
		{
			if (Users.AsNoTracking().FirstOrDefault(u => u.Username == "admin") == null)
			{
				log.Info("Database empty. Adding first admin");
				var admin = new Administrator("admin", "admin", "admin", "admin");
				Users.Add(admin);
				SaveChanges();
			}
		}

		#region Planner Methods

		public void AddPlanner(Planner planner)
		{
			Planners.Add(planner);
			SaveChanges();
			log.Info($"Added planner: {planner.Id}|{planner.Name}");
		}

		public Planner GetPlanner(int id)
		{
			var planner = Planners.AsNoTracking().Include(p => p.Events).Where(p => p.Id == id).FirstOrDefault();
			if (planner is null)
			{
				log.Debug($"Planner id={id} not found");
			}
			else
			{
				log.Debug($"Getting planner {planner.Id}");
			}
			return planner;
		}

		public List<Planner> GetPlanners()
		{
			var planners = Planners.AsNoTracking().Include(p => p.Events).ToList();
			log.Debug("Getting all planners");
			return planners;
		}

		public void EditPlanner(Planner planner)
		{
			var oldPlanner = Planners.FirstOrDefault(p => p.Id == planner.Id);
			Entry(oldPlanner).CurrentValues.SetValues(planner);
			SaveChanges();
			log.Info($"Edited planner: {planner.Id}|{planner.Name}");
		}

		public void RemovePlanner(int plannerId)
		{
			Planners.Remove(Planners.Include(p => p.Events).FirstOrDefault(p => p.Id == plannerId));
			SaveChanges();
			log.Info($"Removed planner: id={plannerId}");
		}

		#endregion

		#region User Methods

		public void AddUser(User user)
		{
			Users.Add(user);
			SaveChanges();
			log.Info($"Added user: {user.Username}|{user.Password}");
		}

		public User GetUser(string username)
		{
			var user = Users.AsNoTracking().Where(u => u.Username == username).FirstOrDefault();
			if (user is null)
			{
				log.Debug($"User username={username} not found");
			}
			else
			{
				log.Debug($"Getting user {user.Username}");
			}
			return user;
		}

		public List<User> GetUsers()
		{
			var users = Users.AsNoTracking().ToList();
			log.Debug("Getting all users");
			return users;
		}

		public void EditUser(User newUser)
		{
			var oldUser = Users.FirstOrDefault(i => i.Username == newUser.Username);
			Entry(oldUser).CurrentValues.SetValues(newUser);
			SaveChanges();
			log.Info($"Edited user: {oldUser.Username}");
		}

		public void RemoveUser(User user)
		{
			Users.Remove(user);
			SaveChanges();
			log.Info($"Removed user: username={user.Username}");
		}

		public bool ExistUser(string username)
		{
			bool retval = Users.AsNoTracking().FirstOrDefault(u => u.Username == username) == null ? false : true;
			log.Debug($"Checking existence of user: {username}");
			return retval;
		}

		#endregion

		#region Events Methods

		public void AddEvent(Event @event, int plannerId)
		{
			Planners.FirstOrDefault(p => p.Id == plannerId).Events.Add(@event);
			Events.Add(@event);
			SaveChanges();
			log.Info($"Added event {@event.Id}|{@event.Name}");
		}

		public Event GetEvent(int id)
		{
			var @event = Events.AsNoTracking().FirstOrDefault(e => e.Id == id);
			if (@event is null)
			{
				log.Debug($"Event id={id} not found");
			}
			else
			{
				log.Debug($"Getting event {@event.Id}");
			}
			return @event;
		}

		public List<Event> GetEvents()
		{
			var events = Events.AsNoTracking().ToList();
			log.Debug("Getting all events");
			return events;
		}

		public void EditEvent(Event @event)
		{
			var oldEvent = Events.FirstOrDefault(e => e.Id == @event.Id);
			Entry(oldEvent).CurrentValues.SetValues(@event);
			SaveChanges();
			log.Info($"Edited event: {oldEvent.Id}");
		}

		public void RemoveEvent(int id)
		{
			Events.Remove(Events.FirstOrDefault(e => e.Id == id));
			SaveChanges();
			log.Info($"Removed event: id={id}");
		}

		#endregion
	}
}