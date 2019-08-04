namespace Client.Model
{
	public interface IMessage
	{
		object GetMessage();
		void SendMessage(object message);
	}
}
