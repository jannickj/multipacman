using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;
using GooseEngine.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
    public class TriggerManager
    {
        private DictionaryList<Type, Trigger> triggers = new DictionaryList<Type, Trigger>();

        public void Raise(GameEvent evt)
        {
            ICollection<Trigger> trigered = triggers.Get(evt.GetType());
            foreach (Trigger t in trigered)
            {
                if (t.CheckCondition(evt))
                    t.Execute(evt);
            }
        }

        public void Register(Trigger trigger)
        {
            foreach (Type evt in trigger.Events)
            {
                triggers.Add(evt, trigger);
            }

            if (trigger is MultiTrigger)
                regMulti((MultiTrigger)trigger);
        }

        public void Deregister(Trigger trigger)
        {
            foreach (Type t in trigger.Events)
            {
                triggers.Remove(t, trigger);
            }

            if (trigger is MultiTrigger)
                deregMulti((MultiTrigger)trigger);
        }

        private void regMulti(MultiTrigger trigger)
        {
            trigger.DeregisteredEvent += trigger_DeregisteredEvent;
            trigger.RegisteredEvent += trigger_RegisteredEvent;
        }

        public void deregMulti(MultiTrigger trigger)
        {
            
            trigger.DeregisteredEvent -= trigger_DeregisteredEvent;
            trigger.RegisteredEvent -= trigger_RegisteredEvent;
        }


        void trigger_RegisteredEvent(object sender, ValueEvent<Type> value)
        {
            triggers.Add(value.Value, (Trigger)sender);
        }


        void trigger_DeregisteredEvent(object sender, ValueEvent<Type> value)
        {
            triggers.Remove(value.Value, (Trigger)sender);
        }
    }
}
