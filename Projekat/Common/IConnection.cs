using System.Collections.Generic;
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

		[OperationContract]
		bool AddUser(User newUser);

		[OperationContract]
		void AddPlanner(Planner planner);

		[OperationContract]
		List<Planner> GetPlanners();

		[OperationContract]
		void AddEvent(Event @event, int plannerId);

		[OperationContract]
		void RemovePlanner(int id);

		[OperationContract]
		Planner GetPlanner(int id);

		[OperationContract]
		void EditPlanner(Planner planner);
	}
}
