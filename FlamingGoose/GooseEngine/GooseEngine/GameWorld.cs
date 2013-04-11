using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Percepts;
using GooseEngine.Data;

namespace GooseEngine
{
    public class GameWorld
    {
        private GameMap map;
        private Dictionary<Entity, Point> entlocs = new Dictionary<Entity, Point>();


        public GameWorld(GameMap map)
        {
            // TODO: Complete member initialization
            this.map = map;
        }

        public Vision View(Point p, int range, Entity entity)
        {
            Vision v = new Vision(map[p.X,p.Y,range], entity);
            return v;

        }

        internal void AddEntity(Point loc, Entity entity)
        {
			entity.World = this;
            entlocs.Add(entity, loc);
            map[loc.X, loc.Y].AddEntity(entity);
        }

        public Point GetEntityPosition(Entity entity)
        {
            return entlocs[entity];
        }

        internal void SetEntityLocation(Point loc, Entity entity)
        {
            
            map[loc.X, loc.Y].AddEntity(entity);
        }
    }
}
