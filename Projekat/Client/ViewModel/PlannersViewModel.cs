using Client.Model;
using Client.View;
using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.ViewModel
{
	public class PlannersViewModel : BindableBase
	{
		//			   method name, parameter
		private Stack<Tuple<string, object, object>> undoStack = new Stack<Tuple<string, object, object>>();
		private Stack<Tuple<string, object, object>> redoStack = new Stack<Tuple<string, object, object>>();

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
		public Command<int> EditEventCommand { get; set; }
		public Command<int> RemoveEventCommand { get; set; }
		public Command UndoCommand { get; set; }
		public Command RedoCommand { get; set; }

		public PlannersViewModel()
		{
			PlannersToShow = new ObservableCollection<Planner>();

			//LoginViewModel.proxy.GetPlanners().ForEach(item => PlannersToShow.Add(item));
			ChangingViewEvents.Instance.PlannersEvent += (sender, e) => { plannersToShow.Clear(); LoginViewModel.proxy.GetPlanners().ForEach(item => PlannersToShow.Add(item)); };
			ChangingViewEvents.Instance.DashboardEvent += (sender, e) => { undoStack.Clear(); redoStack.Clear(); };
			ChangingViewEvents.Instance.LogoutEvent += (sender, e) => { undoStack.Clear(); redoStack.Clear(); };
			ChangingViewEvents.Instance.AddUserEvent += (sender, e) => { undoStack.Clear(); redoStack.Clear(); };

			SearchCommand = new Command<string>(OnSearch);
			AddEventCommand = new Command<int>(OnAddEvent);
			AddPlannerCommand = new Command(OnAddPlanner);
			RemovePlannerCommand = new Command<int>(OnRemovePlanner);
			EditPlannerCommand = new Command<int>(OnEditPlanner);
			DoublePlannerCommand = new Command<int>(OnDoublePlanner);
			EditEventCommand = new Command<int>(OnEditEvent);
			RemoveEventCommand = new Command<int>(OnRemoveEvent);

			UndoCommand = new Command(OnUndo);
			RedoCommand = new Command(OnRedo);
		}

		private void OnRedo()
		{
			var redo = redoStack.Pop();

			string userNameThatRequested = LoginViewModel.factory.Credentials.UserName.UserName;
			switch (redo.Item1)
			{
				case "AddEvent":
					LoginViewModel.proxy.RemoveEvent((int)redo.Item2, userNameThatRequested);
					break;
				case "AddPlanner":
					LoginViewModel.proxy.RemovePlanner((int)redo.Item2, userNameThatRequested);
					break;
				case "RemovePlanner":
					LoginViewModel.proxy.AddPlanner(redo.Item2 as Planner, userNameThatRequested);
					break;
				default:
					break;
			}
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnUndo()
		{
			var undo = undoStack.Pop();

			string userNameThatRequested = LoginViewModel.factory.Credentials.UserName.UserName;
			switch (undo.Item1)
			{
				case "RemoveEvent":
					LoginViewModel.proxy.AddEvent(undo.Item3 as Event, (int)undo.Item2, userNameThatRequested);
					var e = LoginViewModel.proxy.GetPlanner((int)undo.Item2).Events.Last();
					redoStack.Push(new Tuple<string, object, object>("AddEvent", e.Id, null));
					break;
				case "RemovePlanner":
					LoginViewModel.proxy.AddPlanner(undo.Item2 as Planner, userNameThatRequested);
					int p = LoginViewModel.proxy.GetPlanners().Last().Id;
					redoStack.Push(new Tuple<string, object, object>("AddPlanner", p, null));
					break;
				case "AddPlanner":
					int idp = (int)undo.Item2;
					var pp = LoginViewModel.proxy.GetPlanner(idp);
					LoginViewModel.proxy.RemovePlanner(idp, userNameThatRequested);
					redoStack.Push(new Tuple<string, object, object>("RemovePlanner", pp, null));
					break;
				default:
					break;
			}
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnRemoveEvent(int id)
		{
			int planId = -1;
			foreach (var item in plannersToShow)
			{
				foreach (var item2 in item.Events)
				{
					if (item2.Id == id)
					{
						planId = item.Id;
						break;
					}
				}
				if (planId != -1)
				{
					break;
				}
			}

			var e = LoginViewModel.proxy.GetEvent(id);
			undoStack.Push(new Tuple<string, object, object>("RemoveEvent", planId, e));

			LoginViewModel.proxy.RemoveEvent(id, LoginViewModel.factory.Credentials.UserName.UserName);
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnRemovePlanner(int id)
		{
			var p = LoginViewModel.proxy.GetPlanner(id);
			undoStack.Push(new Tuple<string, object, object>("RemovePlanner", p, null));

			LoginViewModel.proxy.RemovePlanner(id, LoginViewModel.factory.Credentials.UserName.UserName);
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnAddPlanner()
		{
			var result = new AddPlannerWindow().ShowDialog();
			if (result.Value)
			{
				var pid = LoginViewModel.proxy.GetPlanners().Last().Id;
				undoStack.Push(new Tuple<string, object, object>("AddPlanner", pid, null));
			}
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnEditEvent(int obj)
		{
			var eve = LoginViewModel.proxy.GetEvent(obj);
			MessageHost.Instance.SendMessage(eve);
			new EditEventWindow().ShowDialog();
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnDoublePlanner(int obj)
		{
			var plan = LoginViewModel.proxy.GetPlanner(obj);
			LoginViewModel.proxy.AddPlanner(plan, LoginViewModel.factory.Credentials.UserName.UserName);
			var id = LoginViewModel.proxy.GetPlanners().Last().Id;
			undoStack.Push(new Tuple<string, object, object>("AddPlanner", id, null));
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnEditPlanner(int obj)
		{
			var planner = LoginViewModel.proxy.GetPlanner(obj);
			MessageHost.Instance.SendMessage(planner);
			new EditPlannerWindow().ShowDialog();
			ChangingViewEvents.Instance.RaisePlannersEvent();
		}

		private void OnAddEvent(int obj)
		{
			var Window = new AddEventWindow();
			MessageHost.Instance.SendMessage(obj);
			Window.ShowDialog();
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
			var plannersList = LoginViewModel.proxy.GetPlanners();
			foreach (var item in plannersList)
			{
				if (item.Name.Contains(obj))
				{
					finded.Add(item);
					continue;
				}
				if (item.Description.Contains(obj))
				{
					finded.Add(item);
					continue;
				}
			}

			PlannersToShow = finded;
		}
	}
}
