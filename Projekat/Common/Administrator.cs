namespace Common
{
	[System.Serializable]
	public class Administrator : User
	{
		public Administrator(string name, string lastname, string username) : base(name, lastname, username)
		{
		}

		public Administrator() : base() { }
	}
}
