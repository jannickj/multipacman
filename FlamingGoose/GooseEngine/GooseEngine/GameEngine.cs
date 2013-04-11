﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine.Data.GenericEvents;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;

namespace GooseEngine
{
    public class GameEngine : GooseObject
    {
        private bool stopEngine;

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
            stopEngine = false;

            while (true)
            {                                
                lock (this.ActionManager)
                {
                    ActionManager.ExecuteActions();
                    if (this.stopEngine)
                        break;
                    Monitor.Wait(this.ActionManager);
                }

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
            value.Value.EventManager = EventManager;
            value.Value.Factory = Factory;
            value.Value.World = World;
            value.Value.ActionManager = ActionManager;
        }

        void actman_ActionQueued(object sender, ValueEvent<GameAction> value)
        {
            lock (this.ActionManager)
            {
                Monitor.PulseAll(this.ActionManager);
            }
        }

        #endregion
    }
}
