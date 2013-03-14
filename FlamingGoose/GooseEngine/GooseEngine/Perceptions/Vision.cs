using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Percepts
{
    public class Vision : Percept
    {
        private List<KeyValuePair<Point, Entity>> entities = new List<KeyValuePair<Point, Entity>>();

        public IEnumerable<KeyValuePair<Point, Entity>> Entities
        {
            get
            {
              
                return entities; 
            }
            

        }


    }
}
