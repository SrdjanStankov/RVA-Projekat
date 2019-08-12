using Client.Model;
using Common;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace Client.ViewModel
{
	public class AddPlannerViewModel : BindableBase
	{
		private Planner planner;
		private string description;

		public string Name { get; set; }
		public string Description
		{
			get => description;
			set
			{
				description = value;
				OnPropertyChanged(nameof(Description));
			}
		}
		public Command<Window> AddPlannerCommand { get; set; }
		public SnackbarMessageQueue MessageQueue { get; set; }

		public Planner Planner
		{
			get => planner;
			set
			{
				planner = value;
				OnPropertyChanged(nameof(Planner));
			}
		}

		public AddPlannerViewModel()
		{
			AddPlannerCommand = new Command<Window>(OnAdd);
			MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
		}

		private void OnAdd(Window window)
		{
			Planner = new Planner(Name, Description);
			Planner.Validate();

			if (!Planner.IsValid)
			{
				if (string.IsNullOrEmpty(Planner.ValidationErrors[nameof(Name)]))
				{
					MessageQueue.Enqueue(Planner.ValidationErrors[nameof(Name)]);
					Planner.ValidationErrors[nameof(Name)] = "*";
				}
				if (string.IsNullOrEmpty(Planner.ValidationErrors[nameof(Description)]))
				{
					MessageQueue.Enqueue(Planner.ValidationErrors[nameof(Description)]);
					Planner.ValidationErrors[nameof(Description)] = "*";
				}
				OnPropertyChanged(nameof(Planner));
				return;
			}

			LoginViewModel.proxy.AddPlanner(Planner, LoginViewModel.factory.Credentials.UserName.UserName);
			LogManager.GetLogger(typeof(AddPlannerViewModel)).Info($"Added planner: id={Planner.Id}");
			window.DialogResult = true;
			window.Close();
		}
	}
}
