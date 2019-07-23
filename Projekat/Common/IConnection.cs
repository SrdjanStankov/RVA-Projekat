using System.ServiceModel;

namespace Common
{
	[ServiceContract]
	public interface IConnection
	{
		[OperationContract]
		void Login(string userName, string password);

		[OperationContract]
		void Logout(string userName);
	}
}
