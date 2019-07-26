using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
	public abstract class ValidationBase : BindableBase
	{
		[NotMapped]
		public ValidationErrors ValidationErrors { get; set; }
		[NotMapped]
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
			OnPropertyChanged("IsValid");
			OnPropertyChanged("ValidationErrors");
		}
	}
}
