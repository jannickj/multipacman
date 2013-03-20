using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Exceptions
{
    public class EntityException : Exception
    {
        private Entity entity;

        public Entity Entity
        {
            get { return entity; }
        }

        public EntityException(Entity entity)
        {
            // TODO: Complete member initialization
            this.entity = entity;
        }
        public EntityException(Entity e, string msg) : base(msg)
        {
            this.entity = e;
        }
       
    }
}
