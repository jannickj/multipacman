using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
    public class GameFactory
    {
        public GameFactory()
        {

        }



        public GameTimer CreateTimer(Action action)
        {
            return new GameTimer(action);
        }
    }
}
