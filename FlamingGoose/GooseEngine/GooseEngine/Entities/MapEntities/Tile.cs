using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Entities.MapEntities
{
    public abstract class Tile : Entity
    {
        public void AddEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        public bool CanContain(Entity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> Entities
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }
}
