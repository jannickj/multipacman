using System;
using System.Collections.Concurrent;
using JSLibrary.Data.GenericEvents;

namespace XmasEngineModel.Management
{
	public class ThreadSafeEventQueue : IDisposable
	{
		private TriggerManager foreignTriggermanager;
		private ConcurrentQueue<XmasEvent> queue = new ConcurrentQueue<XmasEvent>();
		private TriggerManager triggerManager = new TriggerManager();

		public ThreadSafeEventQueue(TriggerManager unsafeTriggers)
		{
			foreignTriggermanager = unsafeTriggers;
			foreignTriggermanager.EventRaised += foreignTriggermanager_EventRaised;
		}

		internal event EventHandler EventRecieved;


		public bool ExecuteNext()
		{
			XmasEvent res;
			if (queue.TryDequeue(out res))
			{
				triggerManager.Raise(res);
				return true;
			}
			return false;
		}


		public void Register(Trigger trigger)
		{
			triggerManager.Register(trigger);
		}

		private void foreignTriggermanager_EventRaised(object sender, UnaryValueEvent<XmasEvent> evt)
		{
			EventHandler buffer = EventRecieved;
			this.queue.Enqueue(evt.Value);

			if (buffer != null)
				buffer(this, new EventArgs());
		}

		public void Dispose()
		{
			foreignTriggermanager.EventRaised -= foreignTriggermanager_EventRaised;
		}
	}
}