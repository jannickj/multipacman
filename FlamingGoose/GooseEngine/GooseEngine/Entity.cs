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
        private RuleHierarchy<Type, Entity> movementRules = new RuleHierarchy<Type, Entity>();
        private LinkedList<Predicate<Entity>> visionBlockRules = new LinkedList<Predicate<Entity>>();

        public Entity()
        {
            conclusions[0] = new Conclusion("Non blocking");
            conclusions[1] = new Conclusion("Blocking");
        }

        protected void AddRuleSuperior<Superior>() where Superior : Entity 
        {
            movementRules.AddLayer(typeof(Superior), new TransformationRule<Entity>());
        }


        private void addrule(RuleHierarchy<Type, Entity> h, Predicate<Entity> p, Conclusion c, Type member)
        {
            TransformationRule<Entity> rules;
            if (h.TryGetRule(member, out rules))
            {
                rules.AddPremise(p, c);
            }
            else
                throw new EntityException(this, "Rules have not been initialized, for this member: \""+member.Name+"\". Use AddRuleSuperior Method to add this member in the decision tree");
        }

        protected void AddWillNotBlock_MovementRule<Member>(Predicate<Entity> otherMember) where Member : Entity
        {
            addrule(movementRules, otherMember, conclusions[0], typeof(Member));
        }

        protected void AddWillBlock_MovementRule<Member>(Predicate<Entity> otherMember) where Member : Entity
        {
            addrule(movementRules, otherMember, conclusions[1], typeof(Member));
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
