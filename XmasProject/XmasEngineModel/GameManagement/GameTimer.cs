using System;
using System.Timers;
using GooseEngine.GameManagement.Actions;

namespace GooseEngine.GameManagement
{
	public class GameTimer
	{
		private Action action;
		private ActionManager actman;
		private bool single;
		private DateTime stopped;
		private Timer timer = new Timer();

		public GameTimer(ActionManager actman, Action action)
		{
			this.actman = actman;
			this.action = action;
			timer.AutoReset = false;

			timer.Elapsed += timer_Elapsed;
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!single)
				timer.Start();

			actman.Queue(new SimpleAction(sa => action()));
		}

		private void start(double m)
		{
			timer.Interval = m;
			timer.Start();
		}

		public void StartSingle(double milisec)
		{
			single = true;
			start(milisec);
		}

		public void StartPeriodic(double milisec)
		{
			single = false;
			start(milisec);
		}

		public void Stop()
		{
			stopped = DateTime.Now;
			timer.Stop();
		}
	}
}