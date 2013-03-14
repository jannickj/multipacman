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
        private Entity entity;
        private float speed;

        /// <summary>
        /// Initializes a move action, which is used to move entities in a gameworld</summary>
        /// <param name="entity"> The entity that gets moved</param>
        /// <param name="direction"> the direction of the move</param>
        /// <param name="speed"> the speed in tiles per second, -1 is instantaneous</param>
        public Move(Entity entity, Enum.Direction direction, float speed): base(entity)
        {
            // TODO: Complete member initialization
            this.entity = entity;
            this.direction = direction;
            this.speed = speed;
        
        }
       

        internal override void Fire()
        {
            
            if (Completed != null)
                Completed(this, new EventArgs());
        }
    }

}
