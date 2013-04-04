using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
    public class ActionManager
    {
        private List<object> wakeup = new List<object>();

        public ActionManager()
        {

        }

        internal void AddWakeUp(object obj)
        {
            this.wakeup.Add(obj);
        }

    }
}
