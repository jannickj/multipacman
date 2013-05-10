using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace XmasEngineView
{
	public abstract class EntityView
	{
		protected ThreadSafeEventQueue eventqueue;
		protected XmasEntity model;
		protected XmasPosition position;

		public EntityView(XmasEntity model)
		{
			this.model = model;
			position = model.Position;
			eventqueue = model.ConstructEventQueue();
			//eventqueue.Register (new Trigger<UnitMovePostEvent> (UnitMoved));
		}

		public ThreadSafeEventQueue EventQueue
		{
			get { return eventqueue; }
		}

		public XmasEntity Model
		{
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