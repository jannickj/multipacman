using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Percepts;

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

        public virtual Vision CreateVisionPercept(Grid<Tile> grid, Entity owner)
        {
            return new Vision(grid, owner);
        }

		public virtual SingleNumeralPercept CreateSingleNumeralPercept (string name, double value)
		{
			return new SingleNumeralPercept (name, value);
		}


        public Thread CreateThread(Action action)
        {
            return new Thread(new ThreadStart(action));
        }
    }
}
