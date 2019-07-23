using System;
using System.ServiceModel;

namespace Server
{
	public class Server
	{
		private ServiceHost host;

		public Server(int port, Type serverType, Type interfaceType)
		{
			host = new ServiceHost(serverType);
			host.AddServiceEndpoint(interfaceType, new NetTcpBinding(), $"net.tcp://localhost:{port}");
		}

		public bool Open()
		{
			try
			{
				host.Open();
				Console.WriteLine($"Server {host.Description.ServiceType} opened...");
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public bool Close()
		{
			try
			{
				host.Close();
				Console.WriteLine($"Server {host.Description.ServiceType} closed...");
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
