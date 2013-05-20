using System;
using System.Collections.Generic;
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
		private XmasModel model;
		private ConsoleWorldView viewWorld;

		public ConsoleView(XmasModel model, ConsoleWorldView viewWorld, ConsoleViewFactory entityFactory, ThreadSafeEventManager evtmanager)
		{
			this.model = model;
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
			Dictionary<Point, ConsoleEntityView> entities = viewWorld.AllEntities();
			DrawSceen screen = new DrawSceen(width, height);

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++ )
					screen[x,y] = ' ';

			foreach (KeyValuePair<Point, ConsoleEntityView> kv in entities)
			{
				int x = kv.Key.X;
				int y = kv.Key.Y;

				screen[x,y] = kv.Value.Symbol;
			}
			return screen.GenerateScreen();
		}

		private void Update()
		{
			DateTime start = DateTime.Now;

			Draw();

			Func<long> remainPct = () => ((DateTime.Now.Ticks - start.Ticks) / 10000) / UPDATE_DELAY;
			while (this.evtmanager.ExecuteNext() && remainPct() <= WORK_PCT)
			{

			}

			long sleeptime = UPDATE_DELAY - ((DateTime.Now.Ticks - start.Ticks) / 10000);
			Console.SetCursorPosition(0, 0);
			long pct = remainPct();

			Console.Write("\rLOAD: " + pct + "%\t\t\t");
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