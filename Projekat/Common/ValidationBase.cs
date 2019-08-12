using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common
{
	[System.Serializable]
	[DataContract]
	public abstract class ValidationBase : BindableBase
	{
		[NotMapped]
		[DataMember]
		public ValidationErrors ValidationErrors { get; set; }

		[NotMapped]
		[DataMember]
		public bool IsValid { get; private set; }

		protected ValidationBase()
		{
			ValidationErrors = new ValidationErrors();
		}

		protected abstract void ValidateSelf();

		public void Validate()
		{
			ValidationErrors.Clear();
			ValidateSelf();
			IsValid = ValidationErrors.IsValid;
			OnPropertyChanged(nameof(IsValid));
			OnPropertyChanged(nameof(ValidationErrors));
		}
	}
}
