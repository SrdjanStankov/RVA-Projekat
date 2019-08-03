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
			AddEventCommand = new Command<int>((obj) => System.Windows.MessageBox.Show(obj.ToString()));
			SearchCommand = new Command<string>((obj) => System.Windows.MessageBox.Show(obj));
			AddPlannerCommand = new Command(OnAddPlanner);
		}

		private void OnAddPlanner()
		{
			var plannerWindow = new AddPlannerWindow();
			plannerWindow.ShowDialog();
		}
	}
}
