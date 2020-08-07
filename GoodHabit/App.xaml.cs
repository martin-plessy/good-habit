using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace GoodHabit
{
	public partial class App : Application
	{
		private void LogUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			string logMessage = e.Exception.ToString();

			try
			{
				string logFilePath = Path.Combine(AppContext.BaseDirectory, $"GoodHabit-Error-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log.txt");

				File.WriteAllText(logFilePath, logMessage);
				MessageBox.Show(
					$"The error was recorded in {logFilePath}",
					"Unexpected Error",
					MessageBoxButton.OK,
					MessageBoxImage.Error
				);
			}
			catch(Exception exception)
			{
				string subMessage = exception.ToString();

				MessageBox.Show(
					$"The error could not be recorded.\n\nOriginal Error: {logMessage}\n\nSubsequent Error: {subMessage}",
					"Multiple Errors",
					MessageBoxButton.OK,
					MessageBoxImage.Error
				);
			}
			
			e.Handled = true;
		}
	}
}
