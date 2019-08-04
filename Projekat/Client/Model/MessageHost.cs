using System;
using System.Collections.Generic;

namespace Client.Model
{
	public class MessageHost : IMessage
	{
		public static MessageHost Instance { get; } = new MessageHost();

		private Queue<object> messages = new Queue<object>();

		private MessageHost() { }

		public object GetMessage()
		{
			try
			{
				return messages.Dequeue();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public void SendMessage(object message)
		{
			messages.Enqueue(message);
		}
	}
}
