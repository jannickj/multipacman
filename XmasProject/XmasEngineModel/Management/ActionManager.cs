using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data.GenericEvents;

namespace XmasEngineModel.Management
{
	public class ActionManager
	{
		private Queue<XmasAction> awaitingActions = new Queue<XmasAction>();
		private HashSet<XmasAction> runningActions = new HashSet<XmasAction>();

		public ICollection<XmasAction> RunningActions
		{
			get { return runningActions.ToArray(); }
		}

		#region EVENTS

		private void action_Completed(object sender, EventArgs e)
		{
			XmasAction ga = (XmasAction) sender;
			runningActions.Remove(ga);
			ga.Completed -= action_Completed;
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
					action.Completed += action_Completed;
					action.Fire();
					actionsExecuted++;
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