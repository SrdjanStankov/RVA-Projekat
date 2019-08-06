using Client.Model;
using Common;
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
				OnPropertyChanged("Description");
			}
		}
		public Command<Window> EditPlannerCommand { get; set; }
		public Planner Planner
		{
			get => planner;
			set
			{
				planner = value;
				OnPropertyChanged("Planner");
			}
		}

		public EditPlannerViewModel()
		{
			EditPlannerCommand = new Command<Window>(OnEdit);
			Planner = MessageHost.Instance.GetMessage() as Planner;
			Name = Planner.Name;
			Description = Planner.Description;
		}

		private void OnEdit(Window window)
		{
			Planner.Name = Name;
			Planner.Description = Description;
			Planner.Validate();

			if (!Planner.IsValid)
			{
				return;
			}

			LoginViewModel.proxy.EditPlanner(Planner, LoginViewModel.factory.Credentials.UserName.UserName);
			window.Close();
		}
	}
}
