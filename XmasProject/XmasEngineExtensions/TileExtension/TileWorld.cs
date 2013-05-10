using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Percepts;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace XmasEngineExtensions.TileExtension
{
	public class TileWorld : XmasWorld
	{
		private Dictionary<XmasEntity, Point> entlocs = new Dictionary<XmasEntity, Point>();
		private TileMap map;

		public TileWorld(TileMap map)
		{
		}

		public TileWorld(Size burstSize)
		{
			map = new TileMap(burstSize);
		}

		public Size Size
		{
			get { return map.Size; }
		}

		public Vision View(Point p, int range, XmasEntity xmasEntity)
		{
			return new Vision(map[p.X, p.Y, range], xmasEntity);
		}

		public Vision View(int range, XmasEntity xmasEntity)
		{
			return View(entlocs[xmasEntity], range, xmasEntity);
		}

		public Vision View(XmasEntity xmasEntity)
		{
			return View(xmasEntity.VisionRange, xmasEntity);
		}

		protected override bool AddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
			TilePosition tilePos = (TilePosition) info.Position;
			return AddEntity(xmasEntity, tilePos);
		}

		private bool AddEntity(XmasEntity xmasEntity, TilePosition pos)
		{
			Point point = pos.Point;

			Tile tile = map[point.X, point.Y];

			if (!tile.CanContain(xmasEntity))
				return false;

			entlocs.Add(xmasEntity, point);
			tile.AddEntity(xmasEntity);
			return true;
		}

		public override XmasPosition GetEntityPosition(XmasEntity xmasEntity)
		{
			return new TilePosition(entlocs[xmasEntity]);
		}

		public override bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition)
		{
			AddEntity(xmasEntity, (TilePosition) tilePosition);

			Point currPoint = entlocs[xmasEntity];
			Point newPoint = ((TilePosition) tilePosition).Point;
			map[currPoint.X, currPoint.Y].RemoveEntity(xmasEntity);
			map[newPoint.X, newPoint.Y].AddEntity(xmasEntity);
			entlocs[xmasEntity] = newPoint;

			return false;
		}

		private bool SetEntityPosition(XmasEntity xmasEntity, TilePosition pos)
		{
			Point point = pos.Point;

			return false;
		}

//
//		internal void SetEntityLocation(Point loc, XmasEntity XmasEntity)
//		{
//			map[loc.X, loc.Y].AddEntity(XmasEntity);
//		}

//		public XmasEntity[] RemoveAllEntities()
//		{
//			XmasEntity[] ents = this.entlocs.Keys.ToArray();
//			foreach (var XmasEntity in ents)
//			{
//				this.RemoveEntity(XmasEntity);
//			}
//			return ents;
//		}

//
//		private IEnumerable<Point> TilesInChunk(Point start, Point stop, ICollection<Point> exceptions)
//		{
//			Point min = new Point(Math.Min(start.X, stop.X), Math.Min(start.Y, stop.Y));
//			Point max = new Point(Math.Max(start.X, stop.X), Math.Max(start.Y, stop.Y));
//
//			for (int x = min.X; x <= max.X; x++)
//			{
//				for (int y = min.Y; y <= max.Y; y++)
//				{
//					if (exceptions != null && !exceptions.Contains(new Point(x, y)))
//						yield return new Point(x, y);
//				}
//			}
//		}
//
//		public void AddChunk<TEntity>(Point start, Point stop)
//			where TEntity : XmasEntity, new()
//		{
//			AddChunkExcept<TEntity>(start, stop, null);
//		}
//
//		public void RemoveChunk<TEntity>(Point start, Point stop)
//			where TEntity : XmasEntity, new()
//		{
//			RemoveChunkExcept<TEntity>(start, stop, null);
//		}
//
//		public void AddChunkExcept<TEntity>(Point start, Point stop, ICollection<Point> exceptions)
//			where TEntity : XmasEntity, new()
//		{
//			foreach (Point p in TilesInChunk(start, stop, exceptions))
//			{
//				TEntity XmasEntity = new TEntity();
//				if (this.map[p.X,p.Y].CanContain(XmasEntity))
//					this.AddEntity(p,XmasEntity);
//			}
//		}
//
//		public void RemoveChunkExcept<TEntity>(Point start, Point stop, ICollection<Point> exceptions)
//			where TEntity : XmasEntity, new()
//		{
//			foreach (Tile tile in TilesInChunk(start, stop, exceptions).Select(p => this.map[p.X,p.Y]))
//			{
//				foreach (TEntity XmasEntity in tile.Entities.OfType<TEntity>().ToArray())
//					this.RemoveEntity(XmasEntity);
//			}
//		}
	}
}