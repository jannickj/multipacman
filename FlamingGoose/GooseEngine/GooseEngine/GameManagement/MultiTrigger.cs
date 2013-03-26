using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
    public class MultiTrigger : Trigger
    {
        private HashSet<Predicate<GameEvent>> conditions = new HashSet<Predicate<GameEvent>>();
        private HashSet<Action<GameEvent>> actions = new HashSet<Action<GameEvent>>();
        private HashSet<Type> eventtypes = new HashSet<Type>();
        private Dictionary<object, Action<GameEvent>> actionDictionary = new Dictionary<object, Action<GameEvent>>();
        private Dictionary<object, Predicate<GameEvent>> condDictionary = new Dictionary<object, Predicate<GameEvent>>();

        internal event ValueHandler<Type> RegisteredEvent;
        internal event ValueHandler<Type> DeregisteredEvent;

        public MultiTrigger()
        {

        }

        public void RegisterEvent<T>() where T : GameEvent
        {
            Type type = typeof(T);
            this.eventtypes.Add(type);
            if (RegisteredEvent != null)
                RegisteredEvent(this, new ValueEvent<Type>(type));

        }

        public override ICollection<Type> Events
        {
            get
            {
                return eventtypes.ToArray();
            }
        }

        public ICollection<Predicate<GameEvent>> Conditions
        {
            get { return conditions.ToArray(); }
        }

        public ICollection<Action<GameEvent>> Actions
        {
            get { return actions.ToArray(); }
        }


        public void DeregisterEvent<T>() where T : GameEvent
        {
            Type type = typeof(T);
            this.eventtypes.Remove(type);
            if (DeregisteredEvent != null)
                DeregisteredEvent(this, new ValueEvent<Type>(type));
        }

        internal void RemoveAction<T>(Action<T> action) where T : GameEvent
        {
            this.removeObject(action, actionDictionary, actions);
        }

        internal void AddAction<T>(Action<T> action) where T : GameEvent
        {
            addObject<Action<GameEvent>>(e => action((T)e), action, this.actionDictionary, this.actions);
        }

        internal void AddCondition<T>(Predicate<T> condition) where T : GameEvent
        {
            addObject<Predicate<GameEvent>>(e => condition((T)e), condition, this.condDictionary, this.conditions);
        }

        internal void RemoveCondition<T>(Predicate<T> condition) where T : GameEvent
        {
            this.removeObject(condition, condDictionary, this.conditions);
        }

        private void addObject<T>(T wrapper, object o, IDictionary<object, T> dic, ICollection<T> list)
        {
            dic.Add(o, wrapper);
            list.Add(wrapper);
            
        }

        private void removeObject<T>(object o, IDictionary<object, T> dic, HashSet<T> list)
        {
            T wrapper;
            if(!dic.TryGetValue(o,out wrapper))
                return;

            dic.Remove(o);

            list.Remove(wrapper);

        }

        internal override bool CheckCondition(GameEvent evt)
        {
            return this.Conditions.All(C => C(evt));
        }

        internal override void Execute(GameEvent evt)
        {
            foreach (Action<GameEvent> A in Actions)
            {
                A(evt);
            }
        }
    }
}
