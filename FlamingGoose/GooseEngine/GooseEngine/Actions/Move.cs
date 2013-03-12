using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GooseEngine.Entities;

namespace GooseEngine.Actions
{
    

    public class Move : GameAction
    {
        private Enum.Direction direction;
        public override event EventHandler Completed;

        public Move(Unit unit, Enum.Direction direction) : base(unit)
        {
            // TODO: Complete member initialization
            this.direction = direction;
        }
       

        internal override void Fire()
        {
            
            if (Completed != null)
                Completed(this, new EventArgs());
        }
    }

}
