using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Entities.Interactables;

namespace GooseEngine.Entities
{
    public abstract class Unit : Entity
    {
        

        public Unit()
        {
            AddWillBlock_MovementRule(p => p is Unit);
            
        }

        
    }
}
