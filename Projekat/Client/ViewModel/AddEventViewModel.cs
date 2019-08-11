using Client.Model;
using Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace Client.ViewModel
{
	public class AddEventViewModel : BindableBase
	{
		private Event @event;

		public string Name { get; set; }
		public string Description { get; set; }
		public Command<Window> AddEventCommand { get; set; }
		public SnackbarMessageQueue MessageQueue { get; set; }

		public Event Event
		{
			get => @event; set
			{
				@event = value;
				OnPropertyChanged("Event");
			}
		}

		public AddEventViewModel()
		{
			AddEventCommand = new Command<Window>(OnAdd);
			MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
		}

		private void OnAdd(Window window)
		{
			Event = new Event(Name, Description);
			Event.Validate();

			if (!Event.IsValid)
			{
				if (Event.ValidationErrors["Name"] != "")
				{
					MessageQueue.Enqueue(Event.ValidationErrors["Name"]);
					Event.ValidationErrors["Name"] = "*";
				}
				if (Event.ValidationErrors["Description"] != "")
				{
					MessageQueue.Enqueue(Event.ValidationErrors["Description"]);
					Event.ValidationErrors["Description"] = "*";
				}
				OnPropertyChanged("Event");
				return;
			}

			LoginViewModel.proxy.AddEvent(Event, (int)MessageHost.Instance.GetMessage(), LoginViewModel.factory.Credentials.UserName.UserName);
			window.DialogResult = true;
			window.Close();
		}
	}
}
