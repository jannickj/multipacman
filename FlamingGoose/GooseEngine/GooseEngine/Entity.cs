using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine
{
    public abstract class Entity
    {

        public virtual bool IsMovementBlocking(Entity entity)
        {
            return false;
        }

        public virtual bool IsVisionBlocking(Entity entity)
        {
            return false;
        }
    }
}
