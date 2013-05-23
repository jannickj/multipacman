using System;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;
using XmasEngineModel.Management.Events;
using System.Collections.Generic;
using ConsoleXmasImplementation.Model;
using XmasEngineExtensions.EisExtension.Model.Events;

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
            evtqueue.Register(new Trigger<EisAgentDisconnectedEvent>(controller_AgentDisconnected));
		}

        private void controller_AgentDisconnected(EisAgentDisconnectedEvent evt)
        {
            log.LogStringWithTimeStamp(string.Format("{{{0}}}'s Controller was disconnected", evt.Agent), DebugLevel.Error);
        }

		private void model_EntityAdded(EntityAddedEvent evt)
		{
			log.LogStringWithTimeStamp(String.Format("{{{0}}} was added to the world", evt.AddedXmasEntity), DebugLevel.Info);
			entities.Add((LoggerEntityView)entityFactory.ConstructEntityView(evt.AddedXmasEntity));
		}

		private void model_EntityRemoved(EntityRemovedEvent evt)
		{

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

