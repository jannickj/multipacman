using System;
using System.Collections.Generic;
using System.Linq;
using GooseEngine.Perceptions;
using JSLibrary.Data;

namespace GooseEngine
{
	public class GooseWorld
	{
		private Dictionary<Entity, Point> entlocs = new Dictionary<Entity, Point>();
		private GooseMap map;


		public GooseWorld(Size burstSize)
		{
			this.map = new GooseMap(burstSize);
			
		}

		public Size Size
		{
			get { return map.Size; }
		}

		public Vision View(Point p, int range, Entity entity)
		{
			return new Vision(map[p.X, p.Y, range], entity);
		}

		public Vision View(int range, Entity entity)
		{
			return View(entlocs[entity], range, entity);
		}

		public Vision View(Entity entity)
		{
			return View(entity.VisionRange, entity);
		}

		protected internal void AddEntity(Point loc, Entity entity)
		{
			entlocs.Add(entity, loc);
			map[loc.X, loc.Y].AddEntity(entity);
		}

		protected internal void RemoveEntity(Entity entity)
		{
			Point p;
			if (this.entlocs.TryGetValue(entity, out p))
			{
				this.map[p.X,p.Y].RemoveEntity(entity);
			}
		}

		public Point GetEntityPosition(Entity entity)
		{
			return entlocs[entity];
		}

		internal void SetEntityLocation(Point loc, Entity entity)
		{
			map[loc.X, loc.Y].AddEntity(entity);
		}

		public Entity[] RemoveAllEntities()
		{
			Entity[] ents = this.entlocs.Keys.ToArray();
			foreach (var entity in ents)
			{
				this.RemoveEntity(entity);
			}
			return ents;
		}


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

		public void AddChunk<TEntity>(Point start, Point stop)
			where TEntity : Entity, new()
		{
			AddChunkExcept<TEntity>(start, stop, null);
		}

		public void RemoveChunk<TEntity>(Point start, Point stop)
			where TEntity : Entity, new()
		{
			RemoveChunkExcept<TEntity>(start, stop, null);
		}

		public void AddChunkExcept<TEntity>(Point start, Point stop, ICollection<Point> exceptions)
			where TEntity : Entity, new()
		{
			foreach (Point p in TilesInChunk(start, stop, exceptions))
			{
				TEntity entity = new TEntity();
				if (this.map[p.X,p.Y].CanContain(entity))
					this.AddEntity(p,entity);
			}
		}

		public void RemoveChunkExcept<TEntity>(Point start, Point stop, ICollection<Point> exceptions)
			where TEntity : Entity, new()
		{
			foreach (Tile tile in TilesInChunk(start, stop, exceptions).Select(p => this.map[p.X,p.Y]))
			{
				foreach (TEntity entity in tile.Entities.OfType<TEntity>().ToArray())
					this.RemoveEntity(entity);
			}
		}
		
	}
}