using System;
using System.Collections.Concurrent;

namespace XmasEngineModel.Management
{
	public class ThreadSafeEventManager
	{
		private ConcurrentQueue<Action> awaitingEvents = new ConcurrentQueue<Action>();

		public void AddEventQueue(ThreadSafeEventQueue queue)
		{
			queue.EventRecieved += queue_EventRecieved;
		}

		private void queue_EventRecieved(object sender, EventArgs e)
		{
			awaitingEvents.Enqueue(() => ((ThreadSafeEventQueue) sender).ExecuteNext());
		}

		public bool ExecuteNext()
		{
			Action a;
			if (awaitingEvents.TryDequeue(out a))
			{
				a();
				return true;
			}
			else
				return false;
		}
	}
}