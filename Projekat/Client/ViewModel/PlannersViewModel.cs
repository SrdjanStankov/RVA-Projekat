using Client.Model;
using Client.View;
using Common;
using System;
using System.Collections.ObjectModel;

namespace Client.ViewModel
{
	public class PlannersViewModel : BindableBase
	{
		private ObservableCollection<Planner> planners;

		public ObservableCollection<Planner> Planners
		{
			get
			{
				planners.Clear();
				try
				{
					LoginViewModel.proxy.GetPlanners().ForEach(item => planners.Add(item));
				}
				catch (Exception) { }
				return planners;
			}

			set
			{
				planners = value;
				OnPropertyChanged("Planners");
			}
		}

		public Command<int> AddEventCommand { get; set; } // parameter is id of planner
		public Command AddPlannerCommand { get; set; }
		public Command<string> SearchCommand { get; set; } // parameter is text for search

		public PlannersViewModel()
		{
			Planners = new ObservableCollection<Planner>();
			SearchCommand = new Command<string>((obj) => System.Windows.MessageBox.Show(obj));
			AddEventCommand = new Command<int>(OnAddEvent);
			AddPlannerCommand = new Command(() => new AddPlannerWindow().ShowDialog());
		}

		private void OnAddEvent(int obj)
		{
			var Window = new AddEventWindow();
			MessageHost.Instance.SendMessage(obj);
			Window.ShowDialog();
		}
	}
}
