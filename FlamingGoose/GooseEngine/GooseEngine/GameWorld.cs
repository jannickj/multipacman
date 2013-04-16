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


        public Size Size
        {
            get
            {
                return map.Size;
            }
        }

        public GameWorld(GameMap map)
        {
            // TODO: Complete member initialization
            this.map = map;
        }

        public Vision View(Point p, int range, Entity entity)
        {
            return new Vision(map[p.X,p.Y,range], entity);
        }

		public Vision View (int range, Entity entity)
		{
			return View (entlocs [entity], range, entity);
		}

		public Vision View (Entity entity)
		{
			return View (entity.VisionRange, entity);
		}

        internal void AddEntity(Point loc, Entity entity)
        {
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
