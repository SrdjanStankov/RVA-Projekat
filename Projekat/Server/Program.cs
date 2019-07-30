using Common;
using System;

namespace Server
{
	internal class Program
	{
		private static Server server = new Server(11223, typeof(Connection), typeof(IConnection));

		private static void Main(string[] args)
		{
			server.Open();
			Console.ReadKey(true);
			server.Close();
		}
	}
}
