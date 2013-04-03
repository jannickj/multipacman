using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Percepts;

namespace GooseEngine
{
    public class GameWorld
    {
        private GameMap map;


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

        public void AddEntity(Point p, Entity entity)
        {
            map[p.X, p.Y].AddEntity(entity);
        }

        public Point GetEntityPosition(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
