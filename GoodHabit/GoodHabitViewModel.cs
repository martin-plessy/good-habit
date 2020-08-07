using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32.TaskScheduler;

namespace GoodHabit
{
	public class GoodHabitViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void ChangeProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (!Equals(value, field))
			{
				field = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private bool IsScheduledValue = false;
		public bool IsScheduled
		{
			get => IsScheduledValue;
			set => ChangeProperty(ref IsScheduledValue, value);
		}

		/// <summary> In minutes. </summary>
		public int RepeatInterval = 40;

		private static readonly Habit[] Habits = new[]
		{
			new Habit { Label = "Blink", Icon = new Uri("Resources/Icons/Blink.png", UriKind.Relative) },
			new Habit { Label = "Roll your eyes", Icon = new Uri("Resources/Icons/Roll.png", UriKind.Relative) },
			new Habit { Label = "Exercise", Icon = new Uri("Resources/Icons/Exercise.png", UriKind.Relative) },
			new Habit { Label = "Walk around", Icon = new Uri("Resources/Icons/Walk.png", UriKind.Relative) },
			new Habit { Label = "Drink water", Icon = new Uri("Resources/Icons/Drink.png", UriKind.Relative) }
		};

		private static readonly Random Random = new Random();

		public Habit Habit { get; } = Habits[Random.Next(0, Habits.Length)];

		private static readonly string ExecutablePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

		public GoodHabitViewModel()
		{
			if (TaskService.Instance.GetTask("GoodHabit") is Task existingScheduledTask)
				if (
					   existingScheduledTask.Definition.Triggers.Count == 1
					&& existingScheduledTask.Definition.Triggers[0].Repetition?.Interval.TotalMinutes is double minutesInterval
					&& 1 <= minutesInterval
					&& existingScheduledTask.Definition.Actions.Count == 1
					&& existingScheduledTask.Definition.Actions[0] is ExecAction execAction
					&& execAction.Path == ExecutablePath
				)
				{
					// A scheduled task exists with consistent data.
					RepeatInterval = (int) minutesInterval;
					IsScheduled = true;
				}
				else
					// A scheduled task exists, but the data seems incorrect.
					RemoveScheduledTask();
		}

		public void RemoveScheduledTask()
		{
			TaskService.Instance.RootFolder.DeleteTask("GoodHabit", exceptionOnNotExists: false);

			RepeatInterval = 40;
			IsScheduled = false;
		}

		/// <param name="repeatInterval"> In minutes. </param>
		public void CreateScheduledTask(int repeatInterval)
		{
			TaskService.Instance.AddTask(
				"GoodHabit",
				new TimeTrigger { Repetition = new RepetitionPattern(TimeSpan.FromMinutes(repeatInterval), default) },
				new ExecAction(ExecutablePath)
			);

			RepeatInterval = repeatInterval;
			IsScheduled = true;
		}
	}
}
