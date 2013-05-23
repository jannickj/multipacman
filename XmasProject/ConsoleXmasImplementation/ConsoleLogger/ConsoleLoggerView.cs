using System;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;
using XmasEngineModel.Management.Events;
using System.Collections.Generic;
using ConsoleXmasImplementation.Model;
using XmasEngineExtensions.EisExtension.Model.Events;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class ConsoleLoggerView : XmasView
	{
		private Dictionary<XmasEntity,LoggerEntityView> viewlookup = new Dictionary<XmasEntity,LoggerEntityView> ();
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
			evtqueue.Register(new Trigger<EntityRemovedEvent>(model_EntityRemoved));
		}

        private void controller_AgentDisconnected(EisAgentDisconnectedEvent evt)
        {
            log.LogStringWithTimeStamp(string.Format("{{{0}}}'s Controller was disconnected ( {1} )", evt.Agent,evt.Exception.Message), DebugLevel.Error);
        }

		private void model_EntityAdded(EntityAddedEvent evt)
		{
			log.LogStringWithTimeStamp(String.Format("{{{0}}} was added to the world", evt.AddedXmasEntity), DebugLevel.Info);
			viewlookup.Add(evt.AddedXmasEntity, (LoggerEntityView)entityFactory.ConstructEntityView(evt.AddedXmasEntity));
		}

		private void model_EntityRemoved(EntityRemovedEvent evt)
		{
			log.LogStringWithTimeStamp(String.Format("{{{0}}} was removed from the world", evt.RemovedXmasEntity), DebugLevel.Info);
			viewlookup[evt.RemovedXmasEntity].Dispose();
			viewlookup.Remove(evt.RemovedXmasEntity);
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

