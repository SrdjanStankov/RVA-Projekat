using System.Runtime.Serialization;

namespace Common
{
	[System.Serializable]
	[DataContract]
	public class Administrator : User
	{
		public Administrator() : base() { }

		public Administrator(string name, string lastname, string username, string password, Planner planner) : base(name, lastname, username, password, planner)
		{
		}
	}
}
