using log4net;
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
				object mess = messages.Dequeue();
				LogManager.GetLogger(typeof(MessageHost)).Debug($"Message sent: {mess}");
				return mess;
			}
			catch (InvalidOperationException e)
			{
				LogManager.GetLogger(typeof(MessageHost)).Error($"Invalid operation", e);
				return null;
			}
		}

		public void SendMessage(object message)
		{
			messages.Enqueue(message);
			LogManager.GetLogger(typeof(MessageHost)).Debug($"Message received");
		}
	}
}
