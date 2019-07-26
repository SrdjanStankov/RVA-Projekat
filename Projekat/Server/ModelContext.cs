namespace Server
{
	using Common;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public class ModelContext : DbContext
	{
		// Your context has been configured to use a 'ModelContext' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'Server.ModelContext' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'ModelContext' 
		// connection string in the application configuration file.
		public ModelContext() : base("name=ModelContext")
		{
			Database.CreateIfNotExists();
		}

		public DbSet<Planner> Planners { get; set; }

		public DbSet<User> Users { get; set; }

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
			user.Planner = Planners.Where(p => p.Id == user.Planner.Id).FirstOrDefault();
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

		#endregion
	}
}