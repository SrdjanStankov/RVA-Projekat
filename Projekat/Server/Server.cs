using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Server
{
	public class Server
	{
		private ServiceHost host;

		public Server(int port, Type serverType, Type interfaceType)
		{
			host = new ServiceHost(serverType);

			var userNameBinding = new NetTcpBinding();
			userNameBinding.Security.Mode = SecurityMode.TransportWithMessageCredential;
			userNameBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

			host.AddServiceEndpoint(interfaceType, userNameBinding, $"net.tcp://localhost:{port}");

			host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");

			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;

			host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
			host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new ServiceAuthenticator();
		}

		public bool Open()
		{
			try
			{
				host.Open();
				Console.WriteLine($"Server {host.Description.ServiceType.Name} opened...");
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
				Console.WriteLine($"Server {host.Description.ServiceType.Name} closed...");
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
