using Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
			var proxy = new ChannelFactory<IConnection>(new NetTcpBinding(), $"net.tcp://localhost:{11223}").CreateChannel();
			proxy.Login(new RegularUser("a", "b", "c"));
		}

		private void ButtonLogout_Click(object sender, RoutedEventArgs e)
		{
			var proxy = new ChannelFactory<IConnection>(new NetTcpBinding(), $"net.tcp://localhost:{11223}").CreateChannel();
			proxy.Logout(new RegularUser("a", "b", "c"));
		}
	}
}
