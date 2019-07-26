using Common;
using System.ServiceModel;
using System.Windows;

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

		}

		private void ButtonLogout_Click(object sender, RoutedEventArgs e)
		{
			var proxy = new ChannelFactory<IConnection>(new NetTcpBinding(), $"net.tcp://localhost:{11223}").CreateChannel();
			proxy.Logout("");
		}
	}
}
