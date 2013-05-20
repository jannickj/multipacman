using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management.Events;

namespace XmasEngineModel.Management
{
	public class ActionManager
	{
		private Queue<XmasAction> awaitingActions = new Queue<XmasAction>();
		private HashSet<XmasAction> runningActions = new HashSet<XmasAction>();
		private EventManager evtman;

		public ActionManager(EventManager evtman)
		{
			this.evtman = evtman;
		}


		public ICollection<XmasAction> RunningActions
		{
			get { return runningActions.ToArray(); }
		}

		public ICollection<XmasAction> QueuedActions
		{
			get { return awaitingActions.ToArray(); }
		}

		#region EVENTS

		private void action_Resolved(object sender, EventArgs e)
		{
			XmasAction ga = (XmasAction) sender;
			runningActions.Remove(ga);
			ga.Resolved -= action_Resolved;
		}

		#endregion

		internal event UnaryValueHandler<XmasAction> ActionQueuing;
		internal event UnaryValueHandler<XmasAction> ActionQueued;


		public int ExecuteActions()
		{
			int actionsExecuted = 0;
			List<XmasAction> actions;

			do
			{
				lock (this)
				{
					actions = awaitingActions.ToList();
					awaitingActions.Clear();
				}

				foreach (XmasAction action in actions)
				{
					runningActions.Add(action);
					action.Resolved += action_Resolved;
					try
					{
						action.Fire();
						actionsExecuted++;
					}
					catch (ForceStopEngineException e)
					{
						throw e;
					}
					catch (Exception e)
					{
						action.Fail();
						this.evtman.Raise(new ActionFailedEvent(e));
					}
					
				}
				lock (this)
				{
					if (awaitingActions.Count == 0)
						break;
				}
			} while (true);
			return actionsExecuted;
		}

		public void Queue(EnvironmentAction action)
		{
			QueueAction(action);
		}

		internal void QueueAction(XmasAction action)
		{
			lock (this)
			{
				if (ActionQueuing != null)
					ActionQueuing(this, new UnaryValueEvent<XmasAction>(action));
				awaitingActions.Enqueue(action);
				if (ActionQueued != null)
					ActionQueued(this, new UnaryValueEvent<XmasAction>(action));
			}
		}
	}
}