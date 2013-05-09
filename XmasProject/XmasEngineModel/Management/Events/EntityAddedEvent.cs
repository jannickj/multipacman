namespace XmasEngineModel.Management.Events
{
	public class EntityAddedEvent : XmasEvent
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

