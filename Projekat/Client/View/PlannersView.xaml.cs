using System.Windows.Controls;

namespace Client.View
{
	/// <summary>
	/// Interaction logic for ViewPlanners.xaml
	/// </summary>
	public partial class ViewPlanners : UserControl
	{
		public ViewPlanners()
		{
			InitializeComponent();
		}

		private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
		{
			SearchBtn.Command.Execute(SearchTb.Text);
		}
	}
}
