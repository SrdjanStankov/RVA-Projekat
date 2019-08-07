using Client.Model;
using Common;

namespace Client.ViewModel
{
	public class AddEventViewModel : BindableBase
	{
		private Event @event;

		public string Name { get; set; }
		public string Description { get; set; }
		public Command<System.Windows.Window> AddEventCommand { get; set; }

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
			AddEventCommand = new Command<System.Windows.Window>(OnAdd);
		}

		private void OnAdd(System.Windows.Window window)
		{
			Event = new Event(Name, Description);
			Event.Validate();

			if (!Event.IsValid)
			{
				return;
			}

			LoginViewModel.proxy.AddEvent(Event, (int)MessageHost.Instance.GetMessage(), LoginViewModel.factory.Credentials.UserName.UserName);
			window.DialogResult = true;
			window.Close();
		}
	}
}
