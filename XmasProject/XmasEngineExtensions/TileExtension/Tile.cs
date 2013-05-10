using System.Collections.Generic;
using System.Linq;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions.TileExtension
{
	public class Tile : XmasObject
	{
		private LinkedList<XmasEntity> entities = new LinkedList<XmasEntity>();

		public ICollection<XmasEntity> Entities
		{
			get { return entities.ToList(); }
		}

		public void AddEntity(XmasEntity xmasEntity)
		{
			entities.AddFirst(xmasEntity);
		}

		public void RemoveEntity(XmasEntity xmasEntity)
		{
			entities.Remove(xmasEntity);
		}

		public bool CanContain(XmasEntity xmasEntity)
		{
			foreach (XmasEntity xent in entities)
			{
				if (xent.IsMovementBlocking(xmasEntity))
					return false;
			}
			return true;
		}

		public bool IsVisionBlocking(XmasEntity xmasEntity)
		{
			return entities.Any(e => e.IsVisionBlocking(xmasEntity));
		}
	}
}