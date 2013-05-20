using System;
using System.Timers;
using XmasEngineModel.Management.Actions;

namespace XmasEngineModel.Management
{
	public class XmasTimer
	{
		private Action action;
		private ActionManager actman;
		private bool single;
		private DateTime stopped;
		private Timer timer = new Timer();
		private XmasAction owner;

		public XmasTimer(ActionManager actman, XmasAction owner, Action action)
		{
			this.owner = owner;
			this.actman = actman;
			this.action = action;
			timer.AutoReset = false;

			timer.Elapsed += timer_Elapsed;
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!single)
				timer.Start();

			SimpleAction sa = new SimpleAction(_ => action());
			sa.Failed += simpleAction_Failed;

			actman.Queue(sa);
		}

		void simpleAction_Failed(object sender, EventArgs e)
		{
			this.owner.Fail();

			((SimpleAction) sender).Failed -= simpleAction_Failed;
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