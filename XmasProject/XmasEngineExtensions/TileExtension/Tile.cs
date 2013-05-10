using System.Collections.Generic;
using System.Linq;
using XmasEngineModel;

namespace XmasEngineExtensions.TileExtension
{
	public class Tile : XmasObject
	{
		private LinkedList<Entity> entities = new LinkedList<Entity>();

		public ICollection<Entity> Entities
		{
			get { return entities.ToList(); }
		}

		public void AddEntity(Entity entity)
		{
			entities.AddFirst(entity);
		}

		public void RemoveEntity(Entity entity)
		{
			entities.Remove(entity);
		}

		public bool CanContain(Entity entity)
		{
			foreach (Entity xent in entities)
			{
				if (xent.IsMovementBlocking(entity))
					return false;
			}
			return true;
		}

		public bool IsVisionBlocking(Entity entity)
		{
			return entities.Any(e => e.IsVisionBlocking(entity));
		}
	}
}