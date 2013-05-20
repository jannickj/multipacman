using System;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;
using XmasEngineModel.Management.Events;
using System.Collections.Generic;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class ConsoleLoggerView : XmasView
	{
		private HashSet<LoggerEntityView> entities = new HashSet<LoggerEntityView> ();
		private Logger log;
		private XmasModel model;
		private LoggerViewFactory entityFactory;
		private ThreadSafeEventManager evtman;
		private ThreadSafeEventQueue evtqueue;

		public ConsoleLoggerView ( XmasModel model
		                         , LoggerViewFactory entityFactory
		                         , ThreadSafeEventManager evtman
		                         , Logger log
		                         )
		{
			this.model = model;
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
			entities.Add((LoggerEntityView)entityFactory.ConstructEntityView(evt.AddedXmasEntity));
			log.LogStringWithTimeStamp (String.Format ("The entity {0} was added to the world"), DebugLevel.AllInformation);
		}

		private void engine_ActionFailed(ActionFailedEvent evt)
		{
			log.LogStringWithTimeStamp (evt.ActionException.Message, DebugLevel.Errors);
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

