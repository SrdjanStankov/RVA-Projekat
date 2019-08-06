using Common;
using System.ServiceModel;
using System.Windows;

namespace Client.Model
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
	public class ConnectionCallback : IConnectionCallback
	{
		public void NotifyChange()
		{
			MessageBox.Show("Change");
		}
	}
}
