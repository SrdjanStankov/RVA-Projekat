using Common;
using System.ServiceModel;
using System.Windows;

namespace Client.Model
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = true)]
	public class ConnectionCallback : IConnectionCallback
	{
		public void NotifyChange()
		{
			//MessageBox.Show("Change");
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}
	}
}
