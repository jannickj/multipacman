using System;
using System.Threading;
using GooseEngine.Perceptions;
using JSLibrary.Data;

namespace GooseEngine.GameManagement
{
	public class GooseFactory
	{
		private ActionManager actman;

		public GooseFactory(ActionManager actman)
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

		public virtual SingleNumeralPercept CreateSingleNumeralPercept(string name, double value)
		{
			return new SingleNumeralPercept(name, value);
		}


		public Thread CreateThread(Action action)
		{
			return new Thread(new ThreadStart(action));
		}
	}
}