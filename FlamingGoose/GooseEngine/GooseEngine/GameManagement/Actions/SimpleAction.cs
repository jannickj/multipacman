using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement.Actions
{
    public class SimpleAction : GameAction
    {
        private Action<SimpleAction> action;

        public SimpleAction(Action<SimpleAction> action)
        {
            this.action = action;
        }

        protected override void Execute()
        {
            this.action(this);
            this.Complete();
        }
    }
}
