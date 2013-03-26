using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
    public abstract class Trigger
    {
        internal virtual event ValueHandler<Type> RegisteredEvent;
        internal virtual event ValueHandler<Type> DeregisteredEvent;

        public abstract ICollection<Type> Events
        {
            get;
        }
        public abstract bool CheckCondition(GameEvent evt);

        internal abstract void Execute(GameEvent evt);
       
    }

    public class Trigger<T> : Trigger where T : GameEvent 
    {
        

        private ICollection<Predicate<T>> conditions;
        private ICollection<Action<T>> actions;
       
        internal Trigger()
        {
            actions = new LinkedList<Action<T>>();
            conditions = new LinkedList<Predicate<T>>();
           
        }

        public Trigger(Action<T> action) : this()
        {
            actions.Add(action);
        }

        public Trigger(Predicate<T> condition, Action<T> action) : this(action)
        {
            this.conditions.Add(condition);
        }


        protected ICollection<Predicate<T>> Conditions
        {
            get { return conditions; }
            set { conditions = value; }
        }

        protected ICollection<Action<T>> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        public override ICollection<Type> Events
        {
            get
            {
                return new Type[] { typeof(T) };
            }
        }

        public override bool CheckCondition(GameEvent evt)
        {
            return conditions.All(c => c((T)evt));
        }

        internal override void Execute(GameEvent evt)
        {
            actions.All(a => { a((T)evt); return true; });
        }
    }
}
