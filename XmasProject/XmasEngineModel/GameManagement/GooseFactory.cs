using System;
using System.Threading;
using GooseEngine.Perceptions;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
	public class GooseFactory
	{
		private ActionManager actman;
		internal event UnaryValueHandler<Tuple<Entity,Point>> EntityCreated; 

		public GooseFactory(ActionManager actman)
		{
			this.actman = actman;
		}

		public GameTimer CreateTimer(Action action)
		{
			GameTimer gt = new GameTimer(actman, action);
			return gt;
		}

		public Entity CreateEntity<TEntity>(Point p) where TEntity : Entity
		{
			TEntity e = Activator.CreateInstance<TEntity>();
			if(EntityCreated!=null)
				EntityCreated(this,new UnaryValueEvent<Tuple<Entity,Point>>(Tuple.Create((Entity)e,p)));
			return e;
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