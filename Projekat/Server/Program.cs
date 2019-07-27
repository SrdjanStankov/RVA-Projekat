using Common;
using System;

namespace Server
{
	class Program
	{
		static Server server = new Server(11223, typeof(Connection), typeof(IConnection));

		static void Main(string[] args)
		{
			server.Open();
			Console.ReadKey(true);
			server.Close();
		}
	}
}
