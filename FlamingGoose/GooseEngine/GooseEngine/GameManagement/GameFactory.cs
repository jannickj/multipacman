using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
    public class GameFactory
    {
        private ActionManager actman;

        public GameFactory(ActionManager actman)
        {
            this.actman = actman;
        }

        public GameTimer CreateTimer(Action action)
        {
            GameTimer gt = new GameTimer(actman, action);
            return gt;
        }

    }
}
