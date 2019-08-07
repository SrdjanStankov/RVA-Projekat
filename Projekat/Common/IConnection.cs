using System.Collections.Generic;
using System.ServiceModel;

namespace Common
{
	[ServiceContract(CallbackContract = typeof(IConnectionCallback), SessionMode = SessionMode.Required)]
	public interface IConnection
	{
		[OperationContract(IsOneWay = true)]
		void Login(string userName, string password);

		[OperationContract(IsOneWay = true)]
		void Change(string userName);

		[OperationContract(IsOneWay = true)]
		void Logout(string userName);

		[OperationContract(IsOneWay = true)]
		void ChangeUserData(User newUser);

		[OperationContract]
		User GetUser(string username);

		[OperationContract]
		bool AddUser(User newUser);

		[OperationContract(IsOneWay = true)]
		void AddPlanner(Planner planner, string usernameThatAdded);

		[OperationContract]
		List<Planner> GetPlanners();

		[OperationContract(IsOneWay = true)]
		void AddEvent(Event @event, int plannerId, string usernameThatAdded);

		[OperationContract(IsOneWay = true)]
		void RemovePlanner(int id, string usernameThatAdded);

		[OperationContract]
		void EditEvent(Event @event, string usernameThatAdded);

		[OperationContract]
		Event GetEvent(int id);

		[OperationContract]
		Planner GetPlanner(int id);

		[OperationContract(IsOneWay = true)]
		void EditPlanner(Planner planner, string usernameThatAdded);

		[OperationContract(IsOneWay = true)]
		void RemoveEvent(int id, string usernameThatAdded);
	}
}
