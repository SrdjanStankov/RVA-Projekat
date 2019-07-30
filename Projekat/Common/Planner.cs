namespace Common
{
	[System.Serializable]
	public class Planner
	{
		public int Id { get; set; }

		public System.Collections.Generic.List<Event> Events { get; set; }
	}
}
