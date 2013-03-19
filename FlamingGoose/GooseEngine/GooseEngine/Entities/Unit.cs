using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Entities.Interactables;

namespace GooseEngine.Entities
{
    public abstract class Unit : Entity
    {
        private bool IMMOVEMENTBLOCKING = false;

        public Unit()
        {
            AddMovementPred(p => p is PowerUp);
            AddMovementPred(p => !this.IMMOVEMENTBLOCKING);
        }

        public override bool IsMovementBlocking(Entity entity)
        {
            
            return true;
        }
    }
}
