using System;
using System.Windows;

namespace GoodHabit
{
	public partial class MainWindow : Window
	{
		public static readonly Habit[] Habits = new[]
		{
			new Habit { Label = "Blink", Icon = new Uri("Resources/Icons/Blink.png", UriKind.Relative) },
			new Habit { Label = "Roll your eyes", Icon = new Uri("Resources/Icons/Roll.png", UriKind.Relative) },
			new Habit { Label = "Exercise", Icon = new Uri("Resources/Icons/Exercise.png", UriKind.Relative) },
			new Habit { Label = "Walk around", Icon = new Uri("Resources/Icons/Walk.png", UriKind.Relative) },
			new Habit { Label = "Drink water", Icon = new Uri("Resources/Icons/Drink.png", UriKind.Relative) }
		};

		public static readonly Random Random = new Random();

		public MainWindow()
		{
			InitializeComponent();
			DataContext = Habits[Random.Next(0, Habits.Length)];
		}

		private void CloseWindow(object sender, RoutedEventArgs e) =>
			Close();
	}
}
