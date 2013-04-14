using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine.Data.GenericEvents;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;
using GooseEngine.Exceptions;

namespace GooseEngine
{
    public class GameEngine
    {
        private bool stopEngine;
        private GameWorld world;
        private GameFactory factory;
        private ActionManager actman;
        private EventManager evtman;
        private Exception engineCrash = null;

        public GameEngine(GameWorld world, ActionManager actman, EventManager evtman, GameFactory factory)
        {
            this.World = world;
            this.ActionManager = actman;
            this.EventManager = evtman;
            this.Factory = factory;

            this.EventManager.Register(new Trigger<EngineCloseEvent>(evtman_EngineClose));
            this.ActionManager.ActionQueuing += actman_ActionQueuing;
            this.ActionManager.ActionQueued += actman_ActionQueued;

        }

        
        public void Start()
        {
            try
            {

                stopEngine = false;

                while (true)
                {

                    ActionManager.ExecuteActions();
                    if (this.stopEngine)
                        break;
                    lock (this.ActionManager)
                    {
                        Monitor.Wait(this.ActionManager);
                    }

                }
            }
            catch (ForceStopEngineException)
            {

            }
            catch (Exception e)
            {
                this.engineCrash = e;
            }
        }

		public void AddEntity (Entity entity, Point loc)
		{
			entity.ActionManager = ActionManager;
			entity.EventManager = EventManager;
			entity.World = World;
			entity.Factory = Factory;
			World.AddEntity (loc, entity);
		}

		public void AddEntity(Entity entity)
		{
			AddEntity (entity, new Point (0, 0));
		}

        public bool EngineCrashed(out Exception exception)
        {
            if (this.engineCrash != null)
            {
                exception = this.engineCrash;
                return true;
            }

            exception = null;    
            return false;
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

        void actman_ActionQueuing(object sender, UnaryValueEvent<GameAction> evt)
        {
            evt.Value.EventManager = EventManager;
            evt.Value.Factory = Factory;
            evt.Value.World = World;
            evt.Value.ActionManager = ActionManager;
        }

        void actman_ActionQueued(object sender, UnaryValueEvent<GameAction> evt)
        {
            lock (this.ActionManager)
            {
                Monitor.PulseAll(this.ActionManager);
            }
        }

        #endregion

        #region PROPERTIES
        public GameWorld World
        {
            get
            {
                return world;
            }
            internal set
            {
                this.world = value;
            }
        }

        public GameFactory Factory
        {
            get
            {
                return factory;
            }

            internal set
            {
                this.factory = value;
            }
        }


        public EventManager EventManager
        {
            get
            {
                return evtman;
            }
            internal set
            {
                evtman = value;
            }
        }

        public ActionManager ActionManager
        {
            get
            {
                return actman;
            }
            internal set
            {
                actman = value;
            }
        }
        #endregion
    }
}
