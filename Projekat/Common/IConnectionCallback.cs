using System.ServiceModel;

namespace Common
{
	public interface IConnectionCallback
	{
		[OperationContract(IsOneWay = true)]
		void NotifyLogin(string username);

		[OperationContract(IsOneWay = true)]
		void NotifyChange(string username);

		[OperationContract(IsOneWay = true)]
		void NotifyLogout(string username);
	}
}
