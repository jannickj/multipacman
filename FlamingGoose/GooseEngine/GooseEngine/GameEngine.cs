using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine.ActionManagement;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Interfaces;

namespace GooseEngine
{
    public class GameEngine
    {
        private GameManager manager;
        private bool stopEngine;

        public GameEngine(IGameManager manager)
        {
            this.manager = (GameManager)manager;
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
