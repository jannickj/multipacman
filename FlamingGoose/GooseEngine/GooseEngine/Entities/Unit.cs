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
        private int health = 1;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Unit()
        {
            this.AddRuleSuperior<Unit>();
            AddWillBlock_MovementRule<Unit>(p => p is Unit);
            
        }

        
    }
}
