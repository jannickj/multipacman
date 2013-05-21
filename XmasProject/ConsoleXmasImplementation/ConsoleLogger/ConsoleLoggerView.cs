using System;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;
using XmasEngineModel.Management.Events;
using System.Collections.Generic;
using ConsoleXmasImplementation.Model;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class ConsoleLoggerView : XmasView
	{
		private HashSet<LoggerEntityView> entities = new HashSet<LoggerEntityView> ();
		private Logger log;
		private LoggerViewFactory entityFactory;
		private ThreadSafeEventManager evtman;
		private ThreadSafeEventQueue evtqueue;

		public ConsoleLoggerView ( XmasModel model
		                         , LoggerViewFactory entityFactory
		                         , ThreadSafeEventManager evtman
		                         , Logger log
		                         )
		{
			this.entityFactory = entityFactory;
			this.log = log;
			this.evtman = evtman;

			evtqueue = model.EventManager.ConstructEventQueue();
			evtman.AddEventQueue(evtqueue);

			evtqueue.Register (new Trigger<EntityAddedEvent> (model_EntityAdded));
			evtqueue.Register (new Trigger<ActionFailedEvent> (engine_ActionFailed));
		}

		private void model_EntityAdded(EntityAddedEvent evt)
		{
			log.LogStringWithTimeStamp(String.Format("{{{0}}} was added to the world", evt.AddedXmasEntity), DebugLevel.Info);

			//prevent player spam
			//if (evt.AddedXmasEntity is Player)
			//	return;
			entities.Add((LoggerEntityView)entityFactory.ConstructEntityView(evt.AddedXmasEntity));
		}

		private void engine_ActionFailed(ActionFailedEvent evt)
		{
			log.LogStringWithTimeStamp (evt.ActionException.Message, DebugLevel.Error);
		}

		#region implemented abstract members of XmasView

		public override void Start ()
		{
			while (true)
				evtman.ExecuteNextWhenReady();
		}

		#endregion
	}
}

