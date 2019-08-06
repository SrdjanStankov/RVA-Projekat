using Client.Model;
using Client.View;
using Common;
using System;
using System.Collections.ObjectModel;

namespace Client.ViewModel
{
	public class PlannersViewModel : BindableBase
	{
		private ObservableCollection<Planner> plannersToShow;

		public ObservableCollection<Planner> PlannersToShow
		{
			get => plannersToShow;
			set
			{
				plannersToShow = value;
				OnPropertyChanged("PlannersToShow");
			}
		}

		public Command<int> AddEventCommand { get; set; } // parameter is id of planner
		public Command AddPlannerCommand { get; set; }
		public Command<string> SearchCommand { get; set; } // parameter is text for search
		public Command<int> RemovePlannerCommand { get; set; }
		public Command<int> EditPlannerCommand { get; set; }
		public Command<int> DoublePlannerCommand { get; set; }

		public PlannersViewModel()
		{
			PlannersToShow = new ObservableCollection<Planner>();

			//LoginViewModel.proxy.GetPlanners().ForEach(item => PlannersToShow.Add(item));
			ChangingViewEvents.Instance.PlannersEvent += (sender, e) => { plannersToShow.Clear(); LoginViewModel.proxy.GetPlanners().ForEach(item => PlannersToShow.Add(item)); };

			SearchCommand = new Command<string>(OnSearch);
			AddEventCommand = new Command<int>(OnAddEvent);
			AddPlannerCommand = new Command(() => { new AddPlannerWindow().ShowDialog(); ChangingViewEvents.Instance.RaisePlannersEvent(); });
			RemovePlannerCommand = new Command<int>((id) => { LoginViewModel.proxy.RemovePlanner(id, LoginViewModel.factory.Credentials.UserName.UserName); ChangingViewEvents.Instance.RaisePlannersEvent(); });
			EditPlannerCommand = new Command<int>(OnEdit);
			DoublePlannerCommand = new Command<int>(OnDouble);
		}

		private void OnDouble(int obj)
		{
			var plan = LoginViewModel.proxy.GetPlanner(obj);
			plan.Id = 0;
			LoginViewModel.proxy.AddPlanner(plan, LoginViewModel.factory.Credentials.UserName.UserName);
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnEdit(int obj)
		{
			var planner = LoginViewModel.proxy.GetPlanner(obj);
			MessageHost.Instance.SendMessage(planner);
			new EditPlannerWindow().ShowDialog();
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnSearch(string obj)
		{
			if (obj == "")
			{
				PlannersToShow.Clear();
				LoginViewModel.proxy.GetPlanners().ForEach(item => PlannersToShow.Add(item));
				return;
			}
			var finded = new ObservableCollection<Planner>();
			LoginViewModel.proxy.GetPlanners().ForEach(item =>
			{
				if (item.Name.Contains(obj))
				{
					finded.Add(item);
				}
			});

			PlannersToShow = finded;
		}

		private void OnAddEvent(int obj)
		{
			var Window = new AddEventWindow();
			MessageHost.Instance.SendMessage(obj);
			Window.ShowDialog();
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}
	}
}
