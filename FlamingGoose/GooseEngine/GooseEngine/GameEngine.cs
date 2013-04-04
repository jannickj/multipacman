using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine.ActionManagement;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;

namespace GooseEngine
{
    public class GameEngine
    {
        private EventManager manager;
        private bool stopEngine;

        public GameEngine(EventManager manager)
        {
            this.manager = (EventManager)manager;
            this.manager.Register(new Trigger<EngineCloseEvent>(_ => stopEngine = true));

           

        }

        public void Start()
        {
            stopEngine = false;

            while (!stopEngine)
            {
                Thread.Sleep(System.Threading.Timeout.Infinite);
                manager.ExecuteActions();

            }

        }
  
    }
}
