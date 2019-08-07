using Client.Model;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public string Name { get; set; }
		public string Description { get; set; }
		public Command<Window> EditEventCommand { get; set; }

		public EditEventViewModel()
		{
			EditEventCommand = new Command<Window>(OnEdit);
			Event = MessageHost.Instance.GetMessage() as Event;
			Name = Event.Name;
			Description = Event.Description;
		}

		private void OnEdit(Window window)
		{
			Event.Name = Name;
			Event.Description = Description;
			Event.Validate();

			if (!Event.IsValid)
			{
				return;
			}

			LoginViewModel.proxy.EditEvent(Event, LoginViewModel.factory.Credentials.UserName.UserName);
			window.DialogResult = true;
			window.Close();
		}
	}
}
