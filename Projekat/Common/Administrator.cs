namespace Common
{
	[System.Serializable]
	public class Administrator : User
	{
		public Administrator() : base() { }

		public Administrator(string name, string lastname, string username, string password, Planner planner) : base(name, lastname, username, password, planner)
		{
		}
	}
}
