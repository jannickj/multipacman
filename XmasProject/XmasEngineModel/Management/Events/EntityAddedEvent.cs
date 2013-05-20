using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Events
{
	public class EntityAddedEvent : XmasEvent
	{
		private XmasEntity addedXmasEntity;

		public EntityAddedEvent(XmasEntity addedXmasEntity)
		{
			this.addedXmasEntity = addedXmasEntity;
		}

		public XmasEntity AddedXmasEntity
		{
			get { return addedXmasEntity; }
		}
	}
}