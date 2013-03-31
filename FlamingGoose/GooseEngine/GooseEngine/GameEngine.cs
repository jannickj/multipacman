using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine.ActionManagement;
using GooseEngine.GameManagement;
using GooseEngine.Interfaces;

namespace GooseEngine
{
    public class GameEngine
    {
        private GameManager manager;

        public GameEngine(IGameManager manager)
        {
            this.manager = (GameManager)manager;

        }

        public void Start()
        {
            while (true)
            {
                update();
            }

        }

        private void update()
        {
            manager.ExecuteActions();
            Thread.Sleep(System.Threading.Timeout.Infinite);
        }




  
    }
}
