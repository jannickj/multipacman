using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.GameManagement;

namespace GooseEngine.GameManagement
{
    public abstract class GameAction
    {
        private GameWorld world;
        private GameFactory factory;
        private ActionManager actman;
        private EventManager evtman;

        public GameAction()
        { 

        }

        public event EventHandler Completed;

        internal void Fire()
        {
            this.Execute();
        }

        protected abstract void Execute();

        protected void Complete()
        {
            if (Completed != null)
                Completed(this, new EventArgs());
        }


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
    }
}
