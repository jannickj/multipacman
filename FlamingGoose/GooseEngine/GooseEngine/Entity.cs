using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Exceptions;
using GooseEngine.Rule;

namespace GooseEngine
{
    public abstract class Entity
    {
        private Conclusion[] conclusions = new Conclusion[2];
        private RuleHierarch<Type, Entity> movementRules = new RuleHierarch<Type, Entity>();
        private LinkedList<Predicate<Entity>> visionBlockRules = new LinkedList<Predicate<Entity>>();

        public Entity()
        {
            conclusions[0] = new Conclusion("Non blocking");
            conclusions[1] = new Conclusion("Blocking");
        }

        protected void InitializeRuleLayer()
        {
            movementRules.AddLayer(this.GetType(), new TransformationRule<Entity>());
        }


        private void addrule(RuleHierarch<Type, Entity> h, Predicate<Entity> p, Conclusion c)
        {
            TransformationRule<Entity> rules;
            if (h.TryGetRule(this.GetType(), out rules))
            {
                rules.AddPremise(p, c);
            }
            else
                throw new EntityException(this, "Rules have not been initialized");
        }

        protected void AddWillNotBlock_MovementRule(Predicate<Entity> p)
        {
            addrule(movementRules, p, conclusions[0]);
        }

        protected void AddWillBlock_MovementRule(Predicate<Entity> p)
        {
            addrule(movementRules, p, conclusions[1]);
        }


        public bool IsMovementBlocking(Entity entity)
        {
            Conclusion c = movementRules.Conclude(entity);
            
            if (c == conclusions[0])
                return false;
            if (c == conclusions[1])
                return true;

            //If the object dont care it will not be blocking
            return false;
        }

        
        public virtual bool IsVisionBlocking(Entity entity)
        {
            return false;
        }
    }
}
