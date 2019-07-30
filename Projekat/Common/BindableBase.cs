using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Common
{
	[System.Serializable]
	[DataContract]
	public class BindableBase : INotifyPropertyChanged
	{
		protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
		{
			if (Equals(member, val))
			{
				return;
			}

			member = val;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };
	}
}
