using JSLibrary.Data;
using XmasEngineModel;
using XmasEngineModel.GameManagement;
using XmasEngineModel.GameManagement.Events;

namespace XmasEngineView
{
	public abstract class EntityView
	{
		protected Entity model;
		protected Point position;
		protected ThreadSafeEventQueue eventqueue;
		
		public ThreadSafeEventQueue EventQueue
		{
			get { return eventqueue; }
		}
		
		public EntityView(Entity model)
		{
			this.model = model;
			position = model.Position;
			eventqueue = model.ConstructEventQueue ();
			eventqueue.Register (new Trigger<UnitMovePostEvent> (UnitMoved));
		}
		
		public Entity Model {
			get { return model; }
			set { model = value; }
		}
		
		public Point Position
		{
			get { return position; }
		}
		
		protected virtual void UnitMoved(UnitMovePostEvent evt)
		{
			position = evt.NewPos;
		}
	}
}