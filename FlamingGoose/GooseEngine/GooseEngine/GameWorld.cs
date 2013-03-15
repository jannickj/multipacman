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

        public Vision View(Point p, int range)
        {
            Vision v = new Vision();

            

            return v;

        }

        public void AddEntity(Point point, Entity entity)
        {
            throw new NotImplementedException();
        }

        public Point GetEntityPosition(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
