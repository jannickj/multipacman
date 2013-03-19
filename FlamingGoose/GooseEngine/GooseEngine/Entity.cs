using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine
{
    public abstract class Entity
    {

        private LinkedList<Predicate<Entity>> movementBlockRules = new LinkedList<Predicate<Entity>>();
        private LinkedList<Predicate<Entity>> visionBlockRules = new LinkedList<Predicate<Entity>>();


        protected void AddMovementPred(Predicate<Entity> p)
        {
            movementBlockRules.AddFirst(p);
        }

  

        public virtual bool IsMovementBlocking(Entity entity)
        {
            movementBlockRules.Select(p => p(entity));
            return false;
        }

        
        public virtual bool IsVisionBlocking(Entity entity)
        {
            return false;
        }
    }
}
