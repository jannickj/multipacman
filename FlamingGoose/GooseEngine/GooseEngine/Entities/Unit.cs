using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Entities
{
    public abstract class Unit : Entity
    {
        public override bool IsMovementBlocking(Entity entity)
        {
            return true;
        }
    }
}
