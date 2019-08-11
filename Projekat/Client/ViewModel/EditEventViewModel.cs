using Client.Model;
using Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace Client.ViewModel
{
	public class EditEventViewModel : BindableBase
	{
		private Event @event;

		public Event Event
		{
			get => @event;
			set
			{
				@event = value;
				OnPropertyChanged("Event");
			}
		}

		private int plannerId;
		private Event oldEvent;

		public string Name { get; set; }
		public string Description { get; set; }
		public Command<Window> EditEventCommand { get; set; }
		public SnackbarMessageQueue MessageQueue { get; set; }

		public EditEventViewModel()
		{
			EditEventCommand = new Command<Window>(OnEdit);
			Event = MessageHost.Instance.GetMessage() as Event;
			plannerId = (int)MessageHost.Instance.GetMessage();
			oldEvent = new Event(Event.Name, Event.Description);
			Name = Event.Name;
			Description = Event.Description;
			MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
		}

		private void OnEdit(Window window)
		{
			Event.Name = Name;
			Event.Description = Description;
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

			var eve = LoginViewModel.proxy.GetEvent(Event.Id);
			MessageHost.Instance.SendMessage(eve);
			if (eve is null)
			{
				if (MessageBox.Show($"The event you are editing is removed.{Environment.NewLine}Do you want to add it again?", "Event Removed!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
				{
					window.DialogResult = false;
					window.Close();
					return;
				}
				else
				{
					LoginViewModel.proxy.AddEvent(Event, plannerId, LoginViewModel.factory.Credentials.UserName.UserName);
					window.DialogResult = true;
					MessageHost.Instance.SendMessage("Add");
					window.Close();
					return;
				}
			}
			else if (eve.Name != oldEvent.Name || eve.Description != oldEvent.Description)
			{
				if (MessageBox.Show($"The event you are editing has changed.{Environment.NewLine}Do you want to change it anyway?", "Event Has Changed!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
				{
					window.DialogResult = false;
					window.Close();
					return;
				}
			}

			LoginViewModel.proxy.EditEvent(Event, LoginViewModel.factory.Credentials.UserName.UserName);
			window.DialogResult = true;
			MessageHost.Instance.SendMessage("Edit");
			window.Close();
		}
	}
}
