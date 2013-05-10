using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineView;

namespace ConsoleXmasImplementation
{
	public abstract class ConsoleEntityView : EntityView
	{
		public ConsoleEntityView(XmasEntity model) : base(model)
		{
			this.model = model;
			//position = model.Position;
			eventqueue = model.ConstructEventQueue();
			eventqueue.Register(new Trigger<UnitMovePostEvent>(UnitMoved));
		}

		public abstract char Symbol { get; }
	}
}