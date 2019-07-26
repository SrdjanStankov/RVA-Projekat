using Client.Model;
using Common;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Client.ViewModel
{
	public class LoginViewModel : BindableBase
	{
		public static IConnection proxy;

		private LoginUser user;
		private ConnectionCallback connectionCallback;

		public Command<object> LoginCommand { get; set; }

		public LoginUser User
		{
			get => user; set
			{
				user = value;
				OnPropertyChanged("User");
			}
		}

		public LoginViewModel()
		{
			User = new LoginUser();
			connectionCallback = new ConnectionCallback();

			LoginCommand = new Command<object>(OnLogin);
		}


		private void OnLogin(object param)
		{
			User.Password = (param as System.Windows.Controls.PasswordBox).Password;
			User.Validate();
			if (!User.IsValid)
			{
				return;
			}

			var binding = new NetTcpBinding();
			binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
			binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

			var factory = new DuplexChannelFactory<IConnection>(connectionCallback, binding, $"net.tcp://localhost:{11223}");
			factory.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
			factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
			factory.Credentials.UserName.UserName = User.Username;
			factory.Credentials.UserName.Password = User.Password;

			proxy = factory.CreateChannel();
			try
			{
				proxy.Login(User.Username, User.Password);
			}
			catch (Exception e)
			{
				User.ValidationErrors[e.InnerException.Message.Split(' ').First()] = e.InnerException.Message;
				OnPropertyChanged("User");
			}
		}
	}
}
