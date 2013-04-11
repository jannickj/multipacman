using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.GameManagement;

namespace GooseEngine.GameManagement
{
    public abstract class GameAction : GooseObject
    {
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
    }
}
