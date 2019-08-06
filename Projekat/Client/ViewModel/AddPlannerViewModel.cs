using Client.Model;
using Common;
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
		}

		private void OnAdd(Window window)
		{
			Planner = new Planner(Name, Description);
			Planner.Validate();

			if (!Planner.IsValid)
			{
				return;
			}

			LoginViewModel.proxy.AddPlanner(Planner, LoginViewModel.factory.Credentials.UserName.UserName);
			window.Close();
		}
	}
}
