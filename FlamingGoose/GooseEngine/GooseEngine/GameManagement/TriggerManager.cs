using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;

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
        }

        public void Deregister(Trigger trigger)
        {
            foreach (Type t in trigger.Events)
            {
                triggers.Remove(t, trigger);
            }
        }
    }
}
