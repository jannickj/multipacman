using System;
using System.Threading;
using GooseEngine.Exceptions;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Interfaces;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;

namespace GooseEngine
{
	public class GooseModel : IStartable
	{
		private ActionManager actman;
		private Exception engineCrash;
		private EventManager evtman;
		private GooseFactory factory;
		private bool stopEngine;
		private GooseWorld world;
        private AutoResetEvent actionRecieved = new AutoResetEvent(false);


		public GooseModel(GooseWorld world, ActionManager actman, EventManager evtman, GooseFactory factory)
		{
			World = world;
			ActionManager = actman;
			EventManager = evtman;
			Factory = factory;

			EventManager.Register(new Trigger<EngineCloseEvent>(evtman_EngineClose));
			ActionManager.ActionQueuing += actman_ActionQueuing;
			ActionManager.ActionQueued += actman_ActionQueued;
		}

		public void Initialize()
		{

		}

		public void Start()
		{
			try
			{
				stopEngine = false;

				while (true)
				{
					ActionManager.ExecuteActions();
                    lock (this)
                    {
                        if (stopEngine)
                            break;
                    }
                    actionRecieved.WaitOne();
				}
			}
			catch (ForceStopEngineException)
			{
			}
			catch (Exception e)
			{
				engineCrash = e;
			}
		}

		public void AddEntity(Entity entity, Point loc)
		{
            AddActor(entity);
			World.AddEntity(loc, entity);
		}

		public void AddEntity(Entity entity)
		{
			AddEntity(entity, new Point(0, 0));
		}

        public void AddActor(GooseActor actor)
        {
            actor.ActionManager = ActionManager;
            actor.EventManager = EventManager;
            actor.World = World;
            actor.Factory = Factory;
        }

		public bool EngineCrashed(out Exception exception)
		{
			if (engineCrash != null)
			{
				exception = engineCrash;
				return true;
			}

			exception = null;
			return false;
		}

		#region EVENTS

		private void evtman_EngineClose(EngineCloseEvent e)
		{
            stopEngine = true;

            this.actionRecieved.Set();
		}

		private void actman_ActionQueuing(object sender, UnaryValueEvent<GameAction> evt)
		{
			evt.Value.EventManager = EventManager;
			evt.Value.Factory = Factory;
			evt.Value.World = World;
			evt.Value.ActionManager = ActionManager;
		}

		private void actman_ActionQueued(object sender, UnaryValueEvent<GameAction> evt)
		{
			lock (ActionManager)
			{
				Monitor.PulseAll(ActionManager);
			}
		}

		#endregion

		#region PROPERTIES

		public GooseWorld World
		{
			get { return world; }
			internal set { world = value; }
		}

		public GooseFactory Factory
		{
			get { return factory; }

			internal set { factory = value; }
		}


		public EventManager EventManager
		{
			get { return evtman; }
			internal set { evtman = value; }
		}

		public ActionManager ActionManager
		{
			get { return actman; }
			internal set { actman = value; }
		}

		#endregion


    
    }
}