using Client.Model;
using Common;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows;

namespace Client.ViewModel
{
	public class LoginViewModel : BindableBase
	{
		public static IConnection proxy;
		public static DuplexChannelFactory<IConnection> factory;

		public Command<object> LoginCommand { get; set; }
		public SnackbarMessageQueue MessageQueue { get; set; }

		public string Username { get; set; }
		public string Password { get; set; }
		public string UsernameError { get; set; }
		public string PasswordError { get; set; }

		public LoginViewModel()
		{
			LoginCommand = new Command<object>(OnLogin);
			ChangingViewEvents.Instance.LogoutEvent += Logout;
			MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
		}

		private void Logout(object sender, EventArgs e)
		{
			try
			{
				proxy.Logout(factory.Credentials.UserName.UserName);
				LogManager.GetLogger(typeof(LoginViewModel)).Info($"Logout successful {factory.Credentials.UserName.UserName}|{factory.Credentials.UserName.Password}");
				factory.Close();
				proxy = null;
				factory = null;
			}
			catch (Exception ee)
			{
				LogManager.GetLogger(typeof(LoginViewModel)).Fatal($"Error logging out.", ee);
			}
		}

		private void OnLogin(object param)
		{
			Password = (param as System.Windows.Controls.PasswordBox).Password;

			#region Login validation

			bool isUsernameValid = true;
			bool isPasswordValid = true;
			UsernameError = "";
			PasswordError = "";

			if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username))
			{
				isUsernameValid = false;
				UsernameError = "*";
				MessageQueue.Enqueue("Username invalid.");
			}

			if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
			{
				isPasswordValid = false;
				PasswordError = "*";
				MessageQueue.Enqueue("Password invalid.");
			}

			OnPropertyChanged(nameof(UsernameError));
			OnPropertyChanged(nameof(PasswordError));

			if (!isUsernameValid || !isPasswordValid)
			{
				return;
			}

			#endregion

			var binding = new NetTcpBinding();
			binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
			binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

			factory = new DuplexChannelFactory<IConnection>(MainWindowViewModel.connectionContext, binding, $"net.tcp://localhost:{11223}");
			factory.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
			factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
			factory.Credentials.UserName.UserName = Username;
			factory.Credentials.UserName.Password = Password;

			proxy = factory.CreateChannel();
			LogManager.GetLogger(typeof(LoginViewModel)).Debug($"Created factory");

			try
			{
				proxy.Login(Username, Password);
				LogManager.GetLogger(typeof(LoginViewModel)).Info($"Login successful {Username}|{Password}");
				ChangingViewEvents.Instance.RaiseUserLoginSuccessful();
				ChangingViewEvents.Instance.RaisePlannersEvent();
				ChangingViewEvents.Instance.RaiseMenuEvent();
			}
			catch (Exception e)
			{
				if (e.InnerException != null)
				{
					MessageQueue.Enqueue($"{e.InnerException.Message}");
					LogManager.GetLogger(typeof(LoginViewModel)).Info($"{e.InnerException.Message}");
				}
				else
				{
					MessageBox.Show(factory.State.ToString(), "State");
					MessageBox.Show(e.Message);
					LogManager.GetLogger(typeof(LoginViewModel)).Fatal($"Unrecognized error", e);
				}
			}
		}
	}
}
