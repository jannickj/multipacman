using System;
using System.Threading;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;
using XmasEngineModel.Management.Events;
using XmasEngineModel.Interfaces;

namespace XmasEngineModel
{
	public class XmasModel : IStartable
	{
		private ActionManager actman;
		private Exception engineCrash;
		private EventManager evtman;
		private XmasFactory factory;
		private bool stopEngine;
		private XmasWorld world;
        private AutoResetEvent actionRecieved = new AutoResetEvent(false);


		public XmasModel(XmasWorld world, ActionManager actman, EventManager evtman, XmasFactory factory)
		{
			World = world;
			ActionManager = actman;
			EventManager = evtman;
			Factory = factory;
			this.factory.EntityCreated += factory_EntityCreated;

			EventManager.Register(new Trigger<EngineCloseEvent>(evtman_EngineClose));
			ActionManager.ActionQueuing += actman_ActionQueuing;
			ActionManager.ActionQueued += actman_ActionQueued;
		}

		void factory_EntityCreated(object sender, UnaryValueEvent<Tuple<Entity, Point>> evt)
		{
			this.AddEntity(evt.Value.Item1,evt.Value.Item2);
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
			this.ActionManager.QueueAction(new SimpleAction(_ => this.EventManager.Raise(new EntityAddedEvent(entity))));
		}

		public void AddEntity(Entity entity)
		{
			AddEntity(entity, new Point(0, 0));
		}

        public void AddActor(XmasActor actor)
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

		private void actman_ActionQueuing(object sender, UnaryValueEvent<XmasAction> evt)
		{
			evt.Value.EventManager = EventManager;
			evt.Value.Factory = Factory;
			evt.Value.World = World;
			evt.Value.ActionManager = ActionManager;
		}

		private void actman_ActionQueued(object sender, UnaryValueEvent<XmasAction> evt)
		{
			lock (ActionManager)
			{
				Monitor.PulseAll(ActionManager);
			}
		}

		#endregion

		#region PROPERTIES

		public XmasWorld World
		{
			get { return world; }
			internal set { world = value; }
		}

		public XmasFactory Factory
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