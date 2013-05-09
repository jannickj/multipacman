using XmasEngineModel;
using XmasEngineModel.GameManagement;
using XmasEngineModel.GameManagement.Events;

namespace XmasEngineView.Console
{
	public abstract class ConsoleEntityView : EntityView
	{
		public ConsoleEntityView(Entity model) : base(model)
		{
			this.model = model;
			position = model.Position;
			eventqueue = model.ConstructEventQueue ();
			eventqueue.Register (new Trigger<UnitMovePostEvent> (UnitMoved));
		}

		public abstract char Symbol { get; }
	}
}