using System.ServiceModel;

namespace Common
{
	[ServiceContract]
	public interface IConnection
	{
		[OperationContract]
		void Login(User user);

		[OperationContract]
		void Logout(User user);
	}
}
