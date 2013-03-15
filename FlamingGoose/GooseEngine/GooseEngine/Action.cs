using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine
{
    public abstract class GameAction
    {
        public abstract event EventHandler Completed;

        public GameAction(Entity entity)
        { 

        }

        internal abstract void Fire();

    }
}
