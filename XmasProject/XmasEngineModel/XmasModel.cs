using System;
using System.Threading;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Interfaces;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace XmasEngineModel
{
	public class XmasModel : IStartable
	{
		private AutoResetEvent actionRecieved = new AutoResetEvent(false);
		private ActionManager actman;
		private Exception engineCrash;
		private EventManager evtman;
		private XmasFactory factory;
		private bool stopEngine;
		private XmasWorld world;


		public XmasModel(XmasWorld world, ActionManager actman, EventManager evtman, XmasFactory factory)
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

		public void AddActor(XmasActor actor)
		{
			actor.ActionManager = ActionManager;
			actor.EventManager = EventManager;
			actor.World = World;
			actor.Factory = Factory;
			;
		}

		#region EVENTS

		private void evtman_EngineClose(EngineCloseEvent e)
		{
			stopEngine = true;

			actionRecieved.Set();
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