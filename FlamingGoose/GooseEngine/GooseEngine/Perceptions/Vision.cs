using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;

namespace GooseEngine.Percepts
{
    public class Vision : Percept
    {
        private List<KeyValuePair<Point, Entity>> entities = new List<KeyValuePair<Point, Entity>>();
        private Grid<Tile> grid;

        public Vision(Grid<Tile> grid)
        {
            this.grid = grid;

        
        }

        public ICollection<KeyValuePair<Point, Entity>> Entities
        {
            get
            {
              
                return entities; 
            }
            

        }


    }
}
