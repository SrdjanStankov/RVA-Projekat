namespace Common
{
	[System.Serializable]
	public class Planner : ValidationBase
	{
		public Planner(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public Planner()
		{

		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public System.Collections.Generic.List<Event> Events { get; set; }

		protected override void ValidateSelf()
		{
			if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
			{
				ValidationErrors["Name"] = "Name is required";
			}

			if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
			{
				ValidationErrors["Description"] = "Description is required";
			}
		}
	}
}
