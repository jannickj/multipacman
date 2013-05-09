using System;
using System.Collections.Generic;
using System.Timers;
using JSLibrary.Data;
using GooseEngine.GameManagement;
using GooseEngine;

namespace GooseEngineView.Console
{
	public class GooseConsoleView : GooseView
	{
		private GooseModel model;
		private ConsoleWorldView viewWorld;
		private ThreadSafeEventQueue eventqueue;
		private ThreadSafeEventManager evtmanager;
		private ConsoleViewFactory entityFactory;

		public GooseConsoleView(GooseModel model, ConsoleWorldView viewWorld, ConsoleViewFactory entityFactory)
		{
			this.model = model;
			this.viewWorld = viewWorld;
			this.entityFactory = entityFactory;
			evtmanager = new ThreadSafeEventManager ();
			eventqueue = model.EventManager.ConstructEventQueue ();
			eventqueue.Register(new Trigger<EntityAddedEvent> (Model_EntityAdded));
		}

		public void Setup()
		{
			
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			draw();
		}

		private void draw()
		{
			;
			System.Console.SetCursorPosition(0, 0);
			System.Console.Write(Area());
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

		public override void Start()
		{
//            System.Console.SetWindowSize(viewWorld.Width, viewWorld.Height);
            Timer timer = new Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 1000 / 25;
            timer.Start();
		}

		private void Model_EntityAdded(EntityAddedEvent evt)
		{
			//TODO: FIX Adding
			//viewWorld.AddEntity (entityFactory.ConstructEntityView (evt.AddedEntity));
		}
	}
}