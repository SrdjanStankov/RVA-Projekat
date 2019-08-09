using Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Server
{
	public class ModelContext : DbContext
	{
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
		}

		public Planner GetPlanner(int id)
		{
			return Planners.AsNoTracking().Include(p => p.Events).Where(p => p.Id == id).FirstOrDefault();
		}

		public List<Planner> GetPlanners()
		{
			return Planners.AsNoTracking().Include(p => p.Events).ToList();
		}

		public void EditPlanner(Planner planner)
		{
			var oldPlanner = Planners.FirstOrDefault(p => p.Id == planner.Id);
			Entry(oldPlanner).CurrentValues.SetValues(planner);
			SaveChanges();
		}

		public void RemovePlanner(int plannerId)
		{
			Planners.Remove(Planners.Include(p => p.Events).FirstOrDefault(p => p.Id == plannerId));
			SaveChanges();
		}

		#endregion

		#region User Methods

		public void AddUser(User user)
		{
			Users.Add(user);
			SaveChanges();
		}

		public User GetUser(string username)
		{
			return Users.AsNoTracking().Where(u => u.Username == username).FirstOrDefault();
		}

		public List<User> GetUsers()
		{
			return Users.AsNoTracking().ToList();
		}

		public void EditUser(User newUser)
		{
			var oldUser = Users.FirstOrDefault(i => i.Username == newUser.Username);
			Entry(oldUser).CurrentValues.SetValues(newUser);
			SaveChanges();
		}

		public void RemoveUser(User user)
		{
			Users.Remove(user);
			SaveChanges();
		}

		public bool ExistUser(string username)
		{
			return Users.AsNoTracking().FirstOrDefault(u => u.Username == username) == null ? false : true;
		}

		#endregion

		#region Events Methods

		public void AddEvent(Event @event, int plannerId)
		{
			Planners.FirstOrDefault(p => p.Id == plannerId).Events.Add(@event);
			Events.Add(@event);
			SaveChanges();
		}

		public Event GetEvent(int id)
		{
			return Events.AsNoTracking().FirstOrDefault(e => e.Id == id);
		}

		public List<Event> GetEvents()
		{
			return Events.AsNoTracking().ToList();
		}

		public void EditEvent(Event @event)
		{
			var oldEvent = Events.FirstOrDefault(e => e.Id == @event.Id);
			Entry(oldEvent).CurrentValues.SetValues(@event);
			SaveChanges();
		}

		public void RemoveEvent(int id)
		{
			Events.Remove(Events.FirstOrDefault(e => e.Id == id));
			SaveChanges();
		}

		#endregion
	}
}