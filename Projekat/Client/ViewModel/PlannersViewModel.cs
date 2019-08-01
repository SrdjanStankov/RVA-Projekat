using Client.Model;
using Common;
using System.Collections.ObjectModel;

namespace Client.ViewModel
{
	public class PlannersViewModel : BindableBase
	{
		private ObservableCollection<Planner> planners;

		public ObservableCollection<Planner> Planners
		{
			get => planners;
			set
			{
				planners = value;
				OnPropertyChanged("Planners");
			}
		}

		public Command<object> BtnCommand { get; set; }

		public PlannersViewModel()
		{
			Planners = new ObservableCollection<Planner>();
			BtnCommand = new Command<object>((obj) => System.Windows.MessageBox.Show((obj as Planner).Id.ToString()));

			Planners.Add(new Planner() { Id = 1, Events = new System.Collections.Generic.List<Event>() { new Event() { Id = 5 }, new Event() { Id = 6 } } });
			Planners.Add(new Planner() { Id = 2 });
			Planners.Add(new Planner() { Id = 3 });
			Planners.Add(new Planner() { Id = 4 });
		}
	}
}
