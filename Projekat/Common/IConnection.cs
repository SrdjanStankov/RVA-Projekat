using System.ServiceModel;

namespace Common
{
	[ServiceContract(CallbackContract = typeof(IConnectionCallback), SessionMode = SessionMode.Required)]
	public interface IConnection
	{
		[OperationContract]
		int Login(string userName, string password);

		[OperationContract]
		void Change(string userName, string password);

		[OperationContract]
		int Logout(string userName);

		[OperationContract]
		void ChangeUserData(User newUser);

		[OperationContract]
		User GetUser(string userName);
	}
}
