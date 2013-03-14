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
        private Tile[,] tiles;


        public GameWorld(Tile[,] tiles)
        {
            // TODO: Complete member initialization
            this.tiles = tiles;
        }

        public Vision View(Point p, int range)
        {
            throw new NotImplementedException();
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
