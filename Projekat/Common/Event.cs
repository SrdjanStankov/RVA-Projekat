namespace Common
{
	[System.Serializable]
	public class Event : ValidationBase
	{
		public Event(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public Event()
		{

		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

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