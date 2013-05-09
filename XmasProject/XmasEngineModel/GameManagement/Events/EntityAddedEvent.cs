namespace XmasEngineModel.GameManagement.Events
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

