using System.Collections.Generic;
using JSLibrary.Data;
using GooseEngine.Perceptions;

namespace GooseEngine
{
	public class GooseWorld
	{
		private Dictionary<Entity, Point> entlocs = new Dictionary<Entity, Point>();
		private GooseMap map;


		public GooseWorld(GooseMap map)
		{
			// TODO: Complete member initialization
			this.map = map;
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

		internal void AddEntity(Point loc, Entity entity)
		{
			entlocs.Add(entity, loc);
			map[loc.X, loc.Y].AddEntity(entity);

		}

		public Point GetEntityPosition(Entity entity)
		{
			return entlocs[entity];
		}

		internal void SetEntityLocation(Point loc, Entity entity)
		{
			map[loc.X, loc.Y].AddEntity(entity);
		}
	}
}