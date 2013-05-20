using System;
using System.Threading;
using JSLibrary.Data;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Percepts;

namespace XmasEngineModel.Management
{
	public class XmasFactory
	{
		private ActionManager actman;

		public XmasFactory(ActionManager actman)
		{
			this.actman = actman;
		}

		internal event UnaryValueHandler<Tuple<XmasEntity, Point>> EntityCreated;

		public XmasTimer CreateTimer(XmasAction owner, Action action)
		{
			XmasTimer gt = new XmasTimer(actman, owner, action);
			return gt;
		}

		public XmasEntity CreateEntity<TEntity>(Point p) where TEntity : XmasEntity
		{
			TEntity e = Activator.CreateInstance<TEntity>();
			if (EntityCreated != null)
				EntityCreated(this, new UnaryValueEvent<Tuple<XmasEntity, Point>>(Tuple.Create((XmasEntity) e, p)));
			return e;
		}

		//TODO: move this to tileworld
//		public virtual Vision CreateVisionPercept(Grid<Tile> grid, XmasEntity owner)
//		{
//			return new Vision(grid, owner);
//		}

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