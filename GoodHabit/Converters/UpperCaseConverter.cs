﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace GoodHabit.Converters
{
	public class UpperCaseConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
			value.ToString().ToUpper(culture);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
