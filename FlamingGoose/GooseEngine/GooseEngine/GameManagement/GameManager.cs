using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.Data;
using GooseEngine.Interfaces;

namespace GooseEngine.ActionManagement
{
    internal class GameManager : IGameManager
    {
       
        private HashSet<Entity> trackedEntities = new HashSet<Entity>();
        private HashSet<GameAction> runningActions = new HashSet<GameAction>();
        private TriggerManager triggerManager = new TriggerManager();
        private Queue<GameAction> awaitingActions = new Queue<GameAction>();
        private GameWorld world;


        internal GameManager()
        {

        }

        public GameManager(GameWorld world)
        {
            this.world = world;
        }
        
        public void ExecuteActions()
        {
            List<GameAction> actions;
            lock(this)
            {
                actions = awaitingActions.ToList();
                awaitingActions.Clear();
            }

            foreach (GameAction action in actions)
            {
                runningActions.Add(action);
                action.Completed += action_Completed;
                action.World = this.world;
                action.Fire(this);
            }
        }

        public void Queue(GameAction action)
        {
            lock (this)
            {
                awaitingActions.Enqueue(action);
                
            }

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

        public GameTimer CreateTimer(Action action)
        {
            return new GameTimer(action);
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
