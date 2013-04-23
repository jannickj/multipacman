using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using JSLibrary.Data;

namespace GooseEngineView.Console
{
	public class EntityView
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