﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GooseEngine.Entities.MapEntities
{
    public class Tile
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
                if (xent.Blocking && entity.Blocking || xent.IsBlocking(entity))
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

    }
}
