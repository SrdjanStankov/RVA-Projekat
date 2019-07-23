using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonLogin_Click(object sender, RoutedEventArgs e)
		{
			var binding = new NetTcpBinding();
			binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
			binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
			//binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


			var factory = new ChannelFactory<IConnection>(binding, $"net.tcp://localhost:{11223}");
			factory.Credentials.UserName.UserName = "pera";
			factory.Credentials.UserName.Password = "pera";
			factory.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
			factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;

			var proxy = factory.CreateChannel();
			proxy.Login("pera","pera");
		}

		private void ButtonLogout_Click(object sender, RoutedEventArgs e)
		{
			var proxy = new ChannelFactory<IConnection>(new NetTcpBinding(), $"net.tcp://localhost:{11223}").CreateChannel();
			proxy.Logout("");
		}
	}
}
