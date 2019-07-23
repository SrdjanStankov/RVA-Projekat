using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		static Server server = new Server(11223, typeof(Connection), typeof(IConnection));

		static void Main(string[] args)
		{
			server.Open();
			Console.ReadKey();
			server.Close();
		}
	}
}
