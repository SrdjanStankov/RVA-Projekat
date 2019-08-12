using Client.Model;
using Common;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace Client.ViewModel
{
	public class EditPlannerViewModel : BindableBase
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
		public Command<Window> EditPlannerCommand { get; set; }
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

		private Planner oldPlanner;

		public EditPlannerViewModel()
		{
			EditPlannerCommand = new Command<Window>(OnEdit);
			Planner = MessageHost.Instance.GetMessage() as Planner;
			oldPlanner = new Planner(Planner.Name, Planner.Description);
			Name = Planner.Name;
			Description = Planner.Description;
			MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
		}

		private void OnEdit(Window window)
		{
			Planner.Name = Name;
			Planner.Description = Description;
			Planner.Validate();

			if (!Planner.IsValid)
			{
				if (string.IsNullOrEmpty(Planner.ValidationErrors["Name"]))
				{
					MessageQueue.Enqueue(Planner.ValidationErrors["Name"]);
					Planner.ValidationErrors["Name"] = "*";
				}
				if (string.IsNullOrEmpty(Planner.ValidationErrors["Description"]))
				{
					MessageQueue.Enqueue(Planner.ValidationErrors["Description"]);
					Planner.ValidationErrors["Description"] = "*";
				}
				OnPropertyChanged(nameof(Planner));
				return;
			}

			var plan = LoginViewModel.proxy.GetPlanner(Planner.Id);
			MessageHost.Instance.SendMessage(plan);
			if (plan is null)
			{
				if (MessageBox.Show($"The planner you are editing is removed.{Environment.NewLine}Do you want to add it again?", "Planner Removed!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
				{
					window.DialogResult = false;
					window.Close();
					return;
				}
				else
				{
					LoginViewModel.proxy.AddPlanner(Planner, LoginViewModel.factory.Credentials.UserName.UserName);
					window.DialogResult = true;
					MessageHost.Instance.SendMessage("Add");
					window.Close();
					return;
				}
			}
			else if (plan.Name != oldPlanner.Name || plan.Description != oldPlanner.Description)
			{
				if (MessageBox.Show($"The planner you are editing has changed.{Environment.NewLine}Do you want to change it anyway?", "Planner Has Changed!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
				{
					window.DialogResult = false;
					window.Close();
					return;
				}
			}

			LoginViewModel.proxy.EditPlanner(Planner, LoginViewModel.factory.Credentials.UserName.UserName);
			window.DialogResult = true;
			MessageHost.Instance.SendMessage("Edit");
			window.Close();
		}
	}
}
