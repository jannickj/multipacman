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

		public ConsoleView(XmasModel model, ConsoleWorldView viewWorld, ConsoleViewFactory entityFactory)
		{
			this.model = model;
			this.viewWorld = viewWorld;
			this.entityFactory = entityFactory;
			evtmanager = new ThreadSafeEventManager();
			eventqueue = model.EventManager.ConstructEventQueue();
			eventqueue.Register(new Trigger<EntityAddedEvent>(Model_EntityAdded));
		}

		public void Setup()
		{
		}

		private void Draw()
		{
			
			Console.SetCursorPosition(0, 0);
			Console.Write(Area());
		}

		public Char[] Area()
		{
			Dictionary<Point, ConsoleEntityView> entities = viewWorld.AllEntities();
			char[] drawchars = new char[viewWorld.Height*viewWorld.Width + viewWorld.Height];

			for (int i = 0; i < viewWorld.Height*viewWorld.Width; i++)
			{
				drawchars[i] = ' ';
			}
			for (int i = 0; i < viewWorld.Height; i++)
			{
				drawchars[viewWorld.Width + i*viewWorld.Height] = '\n';
			}

			foreach (KeyValuePair<Point, ConsoleEntityView> kv in entities)
			{
				int x = kv.Key.X;
				int y = kv.Key.Y;
				int cord = x + y*viewWorld.Width;

				drawchars[cord] = kv.Value.Symbol;
			}
			return drawchars;
		}

		private void Update()
		{
			DateTime start = DateTime.Now;
			Draw();

			Func<int> remainPct =() => ((DateTime.Now.Millisecond - start.Millisecond)*100) / UPDATE_DELAY;
			while (this.evtmanager.ExecuteNext() && remainPct() <= WORK_PCT)
			{
			
			}

			int sleeptime = UPDATE_DELAY - (DateTime.Now.Millisecond - start.Millisecond);
			if(sleeptime > 0)
				Thread.Sleep(sleeptime);
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