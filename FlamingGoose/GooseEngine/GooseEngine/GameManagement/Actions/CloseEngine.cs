using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.ActionManagement;
using GooseEngine.GameManagement.Events;

namespace GooseEngine.GameManagement.Actions
{
    public class CloseEngine : GameAction
    {

        protected override void Execute(EventManager gem)
        {
            gem.Raise(new EngineCloseEvent());
        }
    }
}
