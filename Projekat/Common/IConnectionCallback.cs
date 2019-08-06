using System.ServiceModel;

namespace Common
{
	public interface IConnectionCallback
	{
		[OperationContract(IsOneWay = true)]
		void NotifyChange();
	}
}
