using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine.Data.GenericEvents;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;

namespace GooseEngine
{
    public class GameEngine
    {
        private bool stopEngine;

        private GameWorld world;
        private EventManager evtman;
        private ActionManager actman;
        private GameFactory factory;

        public GameEngine(GameWorld world, ActionManager actman, EventManager evtman, GameFactory factory)
        {
            this.world = world;
            this.actman = actman;
            this.evtman = evtman;
            this.factory = factory;

            this.evtman.Register(new Trigger<EngineCloseEvent>(evtman_EngineClose));
            this.actman.ActionQueuing += actman_ActionQueuing;
            this.actman.ActionQueued += actman_ActionQueued;

        }

        
        public void Start()
        {
            stopEngine = false;

            while (true)
            {                                
                lock (this.actman)
                {
                    actman.ExecuteActions();
                    if (this.stopEngine)
                        break;
                    Monitor.Wait(this.actman);
                }

            }

        }

        #region EVENTS

        void evtman_EngineClose(EngineCloseEvent e)
        {
            stopEngine = true;
            lock (this)
            {
                Monitor.PulseAll(this);
            }
        }

        void actman_ActionQueuing(object sender, ValueEvent<GameAction> value)
        {
            value.Value.EventManager = evtman;
            value.Value.Factory = factory;
            value.Value.World = world;
            value.Value.ActionManager = actman;
        }


        void actman_ActionQueued(object sender, ValueEvent<GameAction> value)
        {
            lock (this.actman)
            {
                Monitor.PulseAll(this.actman);
            }
        }



        #endregion

    }
}
