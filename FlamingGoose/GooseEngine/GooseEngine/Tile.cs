using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GooseEngine
{
    public class Tile : GooseObject
    {
        private LinkedList<Entity> entities = new LinkedList<Entity>();

        public void AddEntity(Entity entity)
        {
            entities.AddFirst(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public bool CanContain(Entity entity)
        {
            foreach (Entity xent in entities)
            {
                if (xent.IsMovementBlocking(entity))
                    return false;
            }
            return true;
        }

        public ICollection<Entity> Entities
        {
            get
            {
                return entities.ToList();
            }
        }

		public bool IsVisionBlocking(Entity entity)
		{
			return entities.Any (e => e.IsVisionBlocking(entity));
		}

    }
}
