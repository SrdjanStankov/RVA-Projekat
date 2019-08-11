using Client.Model;
using Common;
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
				OnPropertyChanged("Description");
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
				OnPropertyChanged("Planner");
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
				if (Planner.ValidationErrors["Name"] != "")
				{
					MessageQueue.Enqueue(Planner.ValidationErrors["Name"]);
					Planner.ValidationErrors["Name"] = "*";
				}
				if (Planner.ValidationErrors["Description"] != "")
				{
					MessageQueue.Enqueue(Planner.ValidationErrors["Description"]);
					Planner.ValidationErrors["Description"] = "*";
				}
				OnPropertyChanged("Planner");
				return;
			}

			LoginViewModel.proxy.AddPlanner(Planner, LoginViewModel.factory.Credentials.UserName.UserName);
			window.DialogResult = true;
			window.Close();
		}
	}
}
