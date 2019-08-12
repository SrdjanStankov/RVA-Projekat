using System.Collections.Generic;

namespace Common
{
	[System.Serializable]
	public class ValidationErrors : BindableBase
	{
		private readonly Dictionary<string, string> validationsErrors = new Dictionary<string, string>();

		public bool IsValid => validationsErrors.Count < 1;

		public string this[string fieldName]
		{
			get => validationsErrors.ContainsKey(fieldName) ? validationsErrors[fieldName] : "";
			set
			{
				if (validationsErrors.ContainsKey(fieldName))
				{
					if (string.IsNullOrWhiteSpace(value))
					{
						validationsErrors.Remove(fieldName);
					}
					else
					{
						validationsErrors[fieldName] = value;
					}
				}
				else
				{
					if (!string.IsNullOrWhiteSpace(value))
					{
						validationsErrors.Add(fieldName, value);
					}
				}
				OnPropertyChanged(nameof(IsValid));
			}
		}

		public void Clear()
		{
			validationsErrors.Clear();
		}
	}
}
