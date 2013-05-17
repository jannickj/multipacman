using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace XmasEngineView
{
	public abstract class EntityView
	{
		protected ThreadSafeEventQueue eventqueue;
		protected XmasEntity model;

		public EntityView(XmasEntity model)
		{
			this.model = model;
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
		}

		public abstract XmasPosition Position { get; protected set; }

		//protected virtual void UnitMoved(UnitMovePostEvent evt)
		//{
		//	position = evt.NewPos;
		//}
	}
}