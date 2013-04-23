using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
	public class ThreadSafeEventManager
	{
		private ConcurrentQueue<Action> awaitingEvents = new ConcurrentQueue<Action>(); 

		public void AddEventQueue(ThreadSafeEventQueue queue)
		{
			queue.EventRecieved += queue_EventRecieved;
		}

		void queue_EventRecieved(object sender, EventArgs e)
		{
			awaitingEvents.Enqueue(() => ((ThreadSafeEventQueue)sender).ExecuteNext());
		}

		public void ExecuteNext()
		{
			Action a;
			if (awaitingEvents.TryDequeue(out a))
				a();

		}
	}
}
