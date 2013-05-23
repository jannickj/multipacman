using System;
using System.Collections.Concurrent;
using System.Threading;

namespace XmasEngineModel.Management
{
	public class ThreadSafeEventManager
	{
		private ConcurrentQueue<Action> awaitingEvents = new ConcurrentQueue<Action>();
		private AutoResetEvent waitForItemEvent = new AutoResetEvent (false);

		public void AddEventQueue(ThreadSafeEventQueue queue)
		{
			queue.EventRecieved += queue_EventRecieved;
		}

		private void queue_EventRecieved(object sender, EventArgs e)
		{
			awaitingEvents.Enqueue (() => ((ThreadSafeEventQueue)sender).ExecuteNext ());
			waitForItemEvent.Set ();
		}

		public bool ExecuteNext()
		{
			Action a;
			bool retval;
			if (awaitingEvents.TryDequeue (out a)) {
				a ();
				retval = true;
			} else {
				retval = false;
			}

			if (!awaitingEvents.IsEmpty)
				waitForItemEvent.Set ();

			return retval;
		}

		public void ExecuteNextWhenReady()
		{
			waitForItemEvent.WaitOne ();
			ExecuteNext ();
		}

		public void ExecuteNextWhenReady(TimeSpan ts)
		{
			waitForItemEvent.WaitOne (ts);
			ExecuteNext ();
		}

        public void ExecuteNextWhenReady(TimeSpan ts, out long slept)
        {
            DateTime start = DateTime.Now;
            waitForItemEvent.WaitOne(ts);
            slept = DateTime.Now.Ticks - start.Ticks;
            ExecuteNext();
        }
	}
}