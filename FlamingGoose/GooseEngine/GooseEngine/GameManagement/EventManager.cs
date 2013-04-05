using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.Data;
using System.Threading;

namespace GooseEngine.GameManagement
{
    public class EventManager 
    {
       
        private HashSet<Entity> trackedEntities = new HashSet<Entity>();
        private TriggerManager triggerManager = new TriggerManager();
        
        internal EventManager()
        {

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

        void entity_TriggerRaised(object sender, GameEvent e)
        {
            this.Raise(e);
        }

        #endregion

    }
}
