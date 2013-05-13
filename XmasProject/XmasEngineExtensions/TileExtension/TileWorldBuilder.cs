using System;
using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions.TileExtension
{
	public class TileWorldBuilder : XmasWorldBuilder
	{
		private Size size;

		public TileWorldBuilder(Size size)
		{
			this.size = size;
		}

		#region implemented abstract members of XmasWorldBuilder

		protected override XmasWorld ConstructWorld ()
		{
			return new TileWorld (size);
		}

		#endregion

		private IEnumerable<Point> TilesInChunk(Point start, Point stop, ICollection<Point> exceptions)
		{
			Point min = new Point(Math.Min(start.X, stop.X), Math.Min(start.Y, stop.Y));
			Point max = new Point(Math.Max(start.X, stop.X), Math.Max(start.Y, stop.Y));

			for (int x = min.X; x <= max.X; x++)
			{
				for (int y = min.Y; y <= max.Y; y++)
				{
					if (exceptions != null && !exceptions.Contains(new Point(x, y)))
						yield return new Point(x, y);
				}
			}
		}

		public void AddChunk (Func<XmasEntity> constructEntity, Point start, Point stop)
		{
			AddChunkExcept(constructEntity, start, stop, null);
		}

		public void AddChunkExcept (Func<XmasEntity> constructEntity, Point start, Point stop, ICollection<Point> exceptions)
		{
			foreach (Point p in TilesInChunk(start, stop, exceptions))
			{
				TileSpawnInformation info = new TileSpawnInformation(new TilePosition (p));
				AddEntity(constructEntity(), info);
			}
		}
		
//		public void RemoveChunk<TEntity>(Point start, Point stop)
//			where TEntity : Entity, new()
//		{
//			RemoveChunkExcept<TEntity>(start, stop, null);
//		}

//		public void RemoveChunkExcept<TEntity>(Point start, Point stop, ICollection<Point> exceptions)
//			where TEntity : Entity, new()
//		{
//			foreach (Point p in TilesInChunk (start, stop, exceptions)) {
//				pointToEntities.
//			}
//
////			foreach (Tile tile in TilesInChunk(start, stop, exceptions).Select(p => this.map[p.X,p.Y]))
////			{
////				foreach (TEntity entity in tile.Entities.OfType<TEntity>().ToArray())
////					this.RemoveEntity(entity);
////			}
//		}
	}
}