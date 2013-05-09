using JSLibrary.Data;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineModel.World;

namespace XmasEngineView
{
	public abstract class EntityView
	{
		protected Entity model;
		protected XmasPosition position;
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
			//eventqueue.Register (new Trigger<UnitMovePostEvent> (UnitMoved));
		}
		
		public Entity Model {
			get { return model; }
			set { model = value; }
		}
		
		public XmasPosition Position
		{
			get { return position; }
		}
		
		//protected virtual void UnitMoved(UnitMovePostEvent evt)
		//{
		//	position = evt.NewPos;
		//}
	}
}