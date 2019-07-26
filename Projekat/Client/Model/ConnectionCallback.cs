using Common;
using System.ServiceModel;
using System.Windows;

namespace Client.Model
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
	public class ConnectionCallback : IConnectionCallback
	{
		public void NotifyChange(string username)
		{
			MessageBox.Show("Change");
		}

		public void NotifyLogin(string username)
		{
			MessageBox.Show("Login");
		}

		public void NotifyLogout(string username)
		{
			MessageBox.Show("Logout");
		}
	}
}
