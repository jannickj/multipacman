using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineView;

namespace XmasEngineExtensions.LoggerExtension
{
	public class LoggerView : XmasView
	{
		private ThreadSafeEventManager evtman;
		private ThreadSafeEventQueue evtq;
		private Logger log;

		public LoggerView(XmasModel model, Logger log)
		{
			this.log = log;
			this.evtman = new ThreadSafeEventManager();
			this.evtq = model.EventManager.ConstructEventQueue();
			this.evtman.AddEventQueue(evtq);
			this.evtq.Register(new Trigger<ActionFailedEvent>(engine_ActionFailed));
		}

		public override void Start()
		{
			while (true)
				evtman.ExecuteNextWhenReady();
		}

		private void engine_ActionFailed(ActionFailedEvent evt)
		{
			log.LogStringWithTimeStamp (evt.ActionException.Message, DebugLevel.Errors);
		}
	}
}
