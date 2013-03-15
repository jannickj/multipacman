using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine
{
    public abstract class Entity
    {
        public virtual bool Blocking
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsBlocking(Entity entity)
        {
            return Blocking;
        }

    }
}
