using System.Collections.Generic;

namespace XmasEngineModel.Management
{
	public class EventManager
	{
		private HashSet<Entity> trackedEntities = new HashSet<Entity>();
		private TriggerManager triggerManager = new TriggerManager();

		public void Raise(XmasEvent evt)
		{
			triggerManager.Raise(evt);
		}

		public void AddEntity(Entity entity)
		{
			trackedEntities.Add(entity);

			entity.TriggerRaised += entity_TriggerRaised;
		}

		public void RemoveEntity(Entity entity)
		{
			trackedEntities.Remove(entity);

			entity.TriggerRaised -= entity_TriggerRaised;
		}

		public void Register(Trigger trigger)
		{
			triggerManager.Register(trigger);
		}

		public void Deregister(Trigger trigger)
		{
			triggerManager.Deregister(trigger);
		}

		public ThreadSafeEventQueue ConstructEventQueue ()
		{
			return new ThreadSafeEventQueue (triggerManager);
		}

		#region EVENTS

		private void entity_TriggerRaised(object sender, XmasEvent e)
		{
			Raise(e);
		}

		#endregion
	}
}