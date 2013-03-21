using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.ActionManagement;

namespace GooseEngine.ActionManagement
{
    public abstract class GameAction
    {        
        public GameAction()
        { 

        }

        internal void Fire(GameEventManager gem)
        {
            this.Execute(gem);
        }

        protected abstract void Execute(GameEventManager gem);

    }
}
