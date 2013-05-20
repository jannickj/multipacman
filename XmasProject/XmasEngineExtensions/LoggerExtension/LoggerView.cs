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
		private StreamWriter logstream;

		public LoggerView(XmasModel model, StreamWriter logstream)
		{
			this.logstream = logstream;
			this.evtman = new ThreadSafeEventManager();
			this.evtq = model.EventManager.ConstructEventQueue();
			this.evtman.AddEventQueue(evtq);
			this.evtq.Register(new Trigger<ActionFailedEvent>(engine_ActionFailed));

		}
		public override void Start()
		{
			while (true)
			{
				evtman.ExecuteNextWhenReady();
			}
			
		}

		private void engine_ActionFailed(ActionFailedEvent evt)
		{
			DateTime date = DateTime.Now;
			string fdate = date.ToString("[dd/MM-yyyy HH:mm:ss]");
			logstream.WriteLine(fdate+": "+evt.ActionException.Message);
			logstream.Flush();
		}
	}
}
