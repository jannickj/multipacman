using System;
using System.Collections.Concurrent;
using JSLibrary.Data.GenericEvents;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
	public class ThreadSafeEventQueue
	{
		private TriggerManager foreignTriggermanager;
		private ConcurrentQueue<GameEvent> queue = new ConcurrentQueue<GameEvent>();
		private TriggerManager triggerManager = new TriggerManager();

		internal event EventHandler EventRecieved;

		public ThreadSafeEventQueue(TriggerManager unsafeTriggers)
		{
			foreignTriggermanager = unsafeTriggers;
			foreignTriggermanager.EventRaised += foreignTriggermanager_EventRaised;
		}

		

		public bool ExecuteNext()
		{
			GameEvent res;
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

		private void foreignTriggermanager_EventRaised(object sender, UnaryValueEvent<GameEvent> evt)
		{
			EventHandler buffer = EventRecieved;

			if (buffer != null)
				buffer(this, new EventArgs());
			
		}
	}
}
