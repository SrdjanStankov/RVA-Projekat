using Common;
using log4net;
using System;

namespace Server
{
	internal class Program
	{
		private static Server server = new Server(11223, typeof(Connection), typeof(IConnection));
		private static ILog log = LogManager.GetLogger(typeof(Program));

		private static void Main()
		{
			log.Debug("Server program started");
			Console.Title = "RVA Projekat - Server";
			server.Open();
			Console.WriteLine("Doing some stuff...");
			using (var ctx = new ModelContext())
			{
				
			}
			Console.WriteLine("Server ready...");
			Console.ReadKey(true);
			server.Close();
			log.Debug("Server program closed");
		}
	}
}
