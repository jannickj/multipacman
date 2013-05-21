using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Timers;
using JSLibrary.Data;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineView;

namespace ConsoleXmasImplementation.View
{
	public class ConsoleView : XmasView
	{
		private const int UPDATE_DELAY = 1000/25;
		private const int WORK_PCT = 90;

		private ConsoleViewFactory entityFactory;
		private ThreadSafeEventQueue eventqueue;
		private ThreadSafeEventManager evtmanager;
		private ConsoleWorldView viewWorld;

		public ConsoleView(XmasModel model, ConsoleWorldView viewWorld, ConsoleViewFactory entityFactory, ThreadSafeEventManager evtmanager)
		{
			this.viewWorld = viewWorld;
			this.entityFactory = entityFactory;

			this.evtmanager = evtmanager;
			eventqueue = model.EventManager.ConstructEventQueue();
			evtmanager.AddEventQueue(eventqueue);
			eventqueue.Register(new Trigger<EntityAddedEvent>(Model_EntityAdded));
		}

		public void Setup()
		{
		}

		private void Draw()
		{
			
			Console.SetCursorPosition(0, 1);
			Console.Write(Area());
		}

		public Char[] Area()
		{
			int width = viewWorld.Width;
			int height = viewWorld.Height;
			DictionaryList<Point, ConsoleEntityView> entities = viewWorld.AllEntities();
			DrawSceen screen = new DrawSceen(width, height);

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++ )
					screen[x,y] = ' ';

			foreach (Point p in entities.Keys)
			{
				int x = p.X;
				int y = p.Y;
				ICollection<ConsoleEntityView> ents;
				if (entities.TryGetValues(p, out ents))
				{
					int count = ents.Count;
					if (count > 1)
						screen[x, y] = count.ToString().ToArray()[0];
					else
						screen[x, y] = ents.First().Symbol;
				}
			}
			return screen.GenerateScreen();
		}

		private void Update()
		{
			DateTime start = DateTime.Now;
			Draw();

			long updateDelayTicks = UPDATE_DELAY * 10000;
			long workTicks = (long)((WORK_PCT / 100.0) * updateDelayTicks);

			Func<long> remainPct = () => ((DateTime.Now.Ticks - start.Ticks) / 100) / UPDATE_DELAY;
			Func<long> remainingTicks = () => workTicks - (DateTime.Now.Ticks - start.Ticks);

//			while (this.evtmanager.ExecuteNext() && remainPct() <= WORK_PCT) { }
			while (remainPct() <= WORK_PCT)
				evtmanager.ExecuteNextWhenReady (new TimeSpan (remainingTicks ()));

			long sleeptime = UPDATE_DELAY - ((DateTime.Now.Ticks - start.Ticks) / 100);
			Console.SetCursorPosition(0, 0);
			long pct = remainPct();

			Console.Write ("LOAD: {0,3}%\t\t\t", pct);
//			Console.Write("\rLOAD: " + pct + "%\t\t\t");
			if (sleeptime > 0)
				Thread.Sleep((int)sleeptime);

		}

		public override void Start()
		{
			while (true)
			{
				Update();
			}	
		}

		

		private void Model_EntityAdded(EntityAddedEvent evt)
		{
			viewWorld.AddEntity((ConsoleEntityView)entityFactory.ConstructEntityView(evt.AddedXmasEntity));
		}
	}
}