using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
	public class ActionManager
	{
		private Queue<GameAction> awaitingActions = new Queue<GameAction>();
		private HashSet<GameAction> runningActions = new HashSet<GameAction>();

		public ICollection<GameAction> RunningActions
		{
			get { return runningActions.ToArray(); }
		}

		#region EVENTS

		private void action_Completed(object sender, EventArgs e)
		{
			GameAction ga = (GameAction) sender;
			runningActions.Remove(ga);
			ga.Completed -= action_Completed;
		}

		#endregion

		internal event UnaryValueHandler<GameAction> ActionQueuing;
		internal event UnaryValueHandler<GameAction> ActionQueued;


		internal int ExecuteActions()
		{
			int actionsExecuted = 0;
			List<GameAction> actions;

			do
			{
				lock (this)
				{
					actions = awaitingActions.ToList();
					awaitingActions.Clear();
				}

				foreach (GameAction action in actions)
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

		internal void QueueAction(GameAction action)
		{
			lock (this)
			{
				if (ActionQueuing != null)
					ActionQueuing(this, new UnaryValueEvent<GameAction>(action));
				awaitingActions.Enqueue(action);
				if (ActionQueued != null)
					ActionQueued(this, new UnaryValueEvent<GameAction>(action));
			}
		}
	}
}