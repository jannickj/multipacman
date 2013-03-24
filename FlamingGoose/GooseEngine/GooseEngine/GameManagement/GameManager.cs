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
    internal class GameManager : IGameManager
    {
       
        private HashSet<Entity> trackedEntities = new HashSet<Entity>();
        private HashSet<GameAction> runningActions = new HashSet<GameAction>();
        private TriggerManager triggerManager = new TriggerManager();
        
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

        public void Raise(GameEvent evt)
        {
            triggerManager.Raise(evt);
        }        

        public void AddEntity(Entity entity)
        {
            this.trackedEntities.Add(entity);

            entity.TriggerRaised += entity_TriggerRaised;
        }

        public void RemoveEntity(Entity entity)
        {
            this.trackedEntities.Remove(entity);

            entity.TriggerRaised -= entity_TriggerRaised;
        }

        public void Register(Trigger trigger)
        {
            this.triggerManager.Register(trigger);
        }

        public void Deregister(Trigger trigger)
        {
            this.triggerManager.Deregister(trigger);
        }

        #region EVENTS

        void action_Completed(object sender, EventArgs e)
        {
            GameAction ga = (GameAction)sender;
            runningActions.Remove(ga);
            ga.Completed -= action_Completed;

        }

        void entity_TriggerRaised(object sender, GameEvent e)
        {
            this.Raise(e);
        }

        #endregion
    }
}
