using System;
using System.Globalization;
using System.Windows.Data;

namespace GoodHabit
{
	public class ReminderConfigurationLabelConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
			value is bool boolValue && boolValue == true
				? "Edit Reminder"
				: "Initialize Reminder";

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
