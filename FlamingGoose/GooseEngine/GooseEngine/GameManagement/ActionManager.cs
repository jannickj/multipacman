using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GooseEngine.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
    public class ActionManager
    {
        internal event UnaryValueHandler<GameAction> ActionQueuing;
        internal event UnaryValueHandler<GameAction> ActionQueued;

        private List<object> wakeup = new List<object>();
        private Queue<GameAction> awaitingActions = new Queue<GameAction>();
        private HashSet<GameAction> runningActions = new HashSet<GameAction>();

        public ActionManager()
        {
            
        }


        internal void ExecuteActions()
        {
           
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
                }
                lock(this)
                {
                    if (awaitingActions.Count == 0)
                        break;
                }
            } while (true);
        }

        public void Queue(EnvironmentAction action)
        {
            this.QueueAction(action);            
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


        public ICollection<GameAction> RunningActions
        {
            get
            {
                return runningActions.ToArray();
            }
        }

        #region EVENTS

        void action_Completed(object sender, EventArgs e)
        {
            GameAction ga = (GameAction)sender;
            runningActions.Remove(ga);
            ga.Completed -= action_Completed;

        }

        #endregion
    }
}
