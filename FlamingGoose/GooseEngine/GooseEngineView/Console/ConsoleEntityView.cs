using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using JSLibrary.Data;

namespace GooseEngineView.Console
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