using System;
using System.Threading;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.Perceptions;

namespace XmasEngineModel.Management
{
	public class XmasFactory
	{
		private ActionManager actman;
		internal event UnaryValueHandler<Tuple<Entity,Point>> EntityCreated; 

		public XmasFactory(ActionManager actman)
		{
			this.actman = actman;
		}

		public XmasTimer CreateTimer(Action action)
		{
			XmasTimer gt = new XmasTimer(actman, action);
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