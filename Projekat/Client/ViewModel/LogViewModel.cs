using Common;
using System.IO;

namespace Client.ViewModel
{
	public class LogViewModel : BindableBase
	{
		private string text;

		public string Text
		{
			get
			{
				string log = File.ReadAllText($"{Directory.GetCurrentDirectory()}//application.log");
				return log;
			}

			set => text = value;
		}
	}
}
