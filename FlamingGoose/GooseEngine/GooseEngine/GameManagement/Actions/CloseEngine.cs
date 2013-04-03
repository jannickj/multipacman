using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.GameManagement.Events;
using GooseEngine.Interfaces;

namespace GooseEngine.GameManagement.Actions
{
    public class CloseEngine : GameAction
    {

        protected override void Execute(IGameManager gem)
        {
            gem.Raise(new EngineCloseEvent());
        }
    }
}
