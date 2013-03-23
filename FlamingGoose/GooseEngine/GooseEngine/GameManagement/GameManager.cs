using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.Data;
using GooseEngine.Interfaces;

namespace GameEngine.ActionManagement
{
    public class GameManager : IGameManager
    {
        private DictionaryList<Type, Trigger> triggers = new DictionaryList<Type, Trigger>();
        private HashSet<GameAction> runningActions = new HashSet<GameAction>();

        internal void RaiseInternal(GameEvent evt)
        {
            ICollection<Trigger> trigered = triggers.Get(evt.GetType());
            foreach (Trigger t in trigered)
            {
                if (t.CheckCondition(evt))
                    t.Execute(evt);
            }
        }

        public void Register(Trigger t)
        {
            foreach (Type evt in t.Events)
            {
                triggers.Add(evt, t);
            }
        }

        public void Execute(GameAction action)
        {
            runningActions.Add(action);
            action.Completed += action_Completed;
            action.Fire(this);
        }

        public ICollection<GameAction> RunningActions
        {
            get
            {
                return runningActions.ToArray();
            }
        }

        void action_Completed(object sender, EventArgs e)
        {
            GameAction ga = (GameAction)sender;
            runningActions.Remove(ga);
            ga.Completed -= action_Completed;

        }

        public void Raise(GameEvent evt)
        {
            this.RaiseInternal(evt);
        }
    }
}
