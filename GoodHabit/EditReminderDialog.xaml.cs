using System.Windows;

namespace GoodHabit
{
	public partial class EditReminderDialog : Window
	{
		public EditReminderDialog(GoodHabitViewModel vm)
		{
			InitializeComponent();

			DataContext = vm;
			RepeatIntervalBox.Text = vm.RepeatInterval.ToString();
		}

		private void SaveReminder(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(RepeatIntervalBox.Text, out int repeatInterval) && 0 < repeatInterval)
			{
				(DataContext as GoodHabitViewModel).CreateScheduledTask(repeatInterval);

				Close();
			}
			else
				MessageBox.Show(
					$"Cannot convert value '{RepeatIntervalBox.Text}' into a number of minutes.",
					"Invalid Input",
					MessageBoxButton.OK,
					MessageBoxImage.Warning
				);
		}

		private void RemoveReminder(object sender, RoutedEventArgs e)
		{
			(DataContext as GoodHabitViewModel).RemoveScheduledTask();

			Close();
		}
	}
}
