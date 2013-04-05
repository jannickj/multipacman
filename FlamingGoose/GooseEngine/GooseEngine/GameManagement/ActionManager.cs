﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GooseEngine.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
    public class ActionManager
    {
        internal event ValueHandler<GameAction> ActionQueuing;
        internal event ValueHandler<GameAction> ActionQueued;

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
                actions = awaitingActions.ToList();
                awaitingActions.Clear();


                foreach (GameAction action in actions)
                {
                    runningActions.Add(action);
                    action.Completed += action_Completed;
                    action.Fire();
                }
            } while (awaitingActions.Count != 0);
        }

        public void Queue(GameAction action)
        {
            lock (this)
            {
                if (ActionQueuing != null)
                    ActionQueuing(this, new ValueEvent<GameAction>(action));
                awaitingActions.Enqueue(action);
                if (ActionQueued != null)
                    ActionQueued(this, new ValueEvent<GameAction>(action));
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
