using System;
using GooseEngine.GameManagement;

namespace GooseEngine
{
	public class EntityAddedEvent : GameEvent
	{
		private Entity addedEntity;

		public Entity AddedEntity {
			get { return addedEntity; }
		}

		public EntityAddedEvent (Entity addedEntity)
		{
			this.addedEntity = addedEntity;
		}
	}
}

