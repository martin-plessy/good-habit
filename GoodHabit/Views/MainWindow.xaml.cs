using System.Windows;
using GoodHabit.ViewModels;

namespace GoodHabit
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new GoodHabitViewModel();
		}

		private void CloseWindow(object sender, RoutedEventArgs e) =>
			Close();

		private void ShowReminderConfigurationDialog(object sender, RoutedEventArgs e) =>
			new EditReminderDialog(DataContext as GoodHabitViewModel) { Owner = this }.Show();
	}
}
