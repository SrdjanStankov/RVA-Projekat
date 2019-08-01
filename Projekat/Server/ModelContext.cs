using Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Server
{
	public class ModelContext : DbContext
	{
		public DbSet<Planner> Planners { get; set; }

		public DbSet<User> Users { get; set; }

		public ModelContext() : base("name=ModelContext")
		{
			Database.CreateIfNotExists();
			AddAdminIfNotExist();
		}

		private void AddAdminIfNotExist()
		{
			if (Users.AsNoTracking().FirstOrDefault(u => u.Username == "admin") == null)
			{
				var admin = new Administrator("admin", "admin", "admin", "admin", null);
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
			return Planners.AsNoTracking().Where(p => p.Id == id).FirstOrDefault();
		}

		public List<Planner> GetPlanners()
		{
			return Planners.AsNoTracking().ToList();
		}

		public void RemovePlanner(Planner planner)
		{
			Planners.Remove(planner);
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
			return Users.AsNoTracking().Include(u => u.Planner).Where(u => u.Username == username).FirstOrDefault();
		}

		public List<User> GetUsers()
		{
			return Users.AsNoTracking().Include(u => u.Planner).ToList();
		}

		public void RemoveUser(User user)
		{
			Users.Remove(user);
			SaveChanges();
		}

		public void ChangeUser(User newUser)
		{
			var oldUser = Users.Include(u => u.Planner).FirstOrDefault(i => i.Username == newUser.Username);
			Entry(oldUser).CurrentValues.SetValues(newUser);
			SaveChanges();
		}

		public bool ExistUser(string username)
		{
			return Users.AsNoTracking().FirstOrDefault(u => u.Username == username) == null ? false : true;
		}

		#endregion
	}
}