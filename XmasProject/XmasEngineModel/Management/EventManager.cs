using System.Collections.Generic;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management
{
	public class EventManager
	{
		private HashSet<XmasEntity> trackedEntities = new HashSet<XmasEntity>();
		private TriggerManager triggerManager = new TriggerManager();

		public void Raise(XmasEvent evt)
		{
			triggerManager.Raise(evt);
		}

		public void AddEntity(XmasEntity xmasEntity)
		{
			trackedEntities.Add(xmasEntity);

			xmasEntity.TriggerRaised += entity_TriggerRaised;
		}

		public void RemoveEntity(XmasEntity xmasEntity)
		{
			trackedEntities.Remove(xmasEntity);

			xmasEntity.TriggerRaised -= entity_TriggerRaised;
		}

		public void Register(Trigger trigger)
		{
			triggerManager.Register(trigger);
		}

		public void Deregister(Trigger trigger)
		{
			triggerManager.Deregister(trigger);
		}

		public ThreadSafeEventQueue ConstructEventQueue()
		{
			return new ThreadSafeEventQueue(triggerManager);
		}

		#region EVENTS

		private void entity_TriggerRaised(object sender, XmasEvent e)
		{
			Raise(e);
		}

		#endregion
	}
}