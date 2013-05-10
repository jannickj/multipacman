using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Events
{
	public class EntityAddedEvent : XmasEvent
	{
		private XmasEntity _addedXmasEntity;

		public EntityAddedEvent(XmasEntity _addedXmasEntity)
		{
			this._addedXmasEntity = _addedXmasEntity;
		}

		public XmasEntity AddedXmasEntity
		{
			get { return _addedXmasEntity; }
		}
	}
}