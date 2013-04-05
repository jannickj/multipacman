using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;

namespace GooseEngine.GameManagement.Actions
{
    public class CloseEngine : GameAction
    {

        protected override void Execute()
        {
            this.EventManager.Raise(new EngineCloseEvent());
            this.Complete();
        }
    }
}
