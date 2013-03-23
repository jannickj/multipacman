using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
    public class MultiTrigger : Trigger<GameEvent>
    {
        private ICollection<Type> eventtypes = new LinkedList<Type>();

        public MultiTrigger()
        {

        }

        public void RegisterEvent<T>() where T : GameEvent
        {
            this.eventtypes.Add(typeof(T));
        }

        public void AddAction(Action<GameEvent> action)
        {
            this.Actions.Add(action);
        }

        public void AddCondition(Predicate<GameEvent> condition)
        {
            this.Conditions.Add(condition);
        }

        public override ICollection<Type> Events
        {
            get
            {
                return eventtypes;
            }
        }

    }
}
