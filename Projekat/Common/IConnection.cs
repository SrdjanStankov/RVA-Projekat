using System.ServiceModel;

namespace Common
{
	[ServiceContract(CallbackContract = typeof(IConnectionCallback), SessionMode = SessionMode.Required)]
	public interface IConnection
	{
		[OperationContract]
		void Login(string userName, string password);

		[OperationContract]
		void Change(string userName, string password);

		[OperationContract]
		void Logout(string userName);

		[OperationContract]
		void ChangeUserData(User newUser);

		[OperationContract]
		User GetUser(string username);
	}
}
