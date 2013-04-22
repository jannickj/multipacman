using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using JSLibrary.Data;

namespace GooseEngineView.Testing.ConsoleView
{
	public abstract class ConsoleEntityView
	{
		private Entity model;
		protected Point position;

		public ConsoleEntityView(Entity model)
		{
			this.model = model;
			position = model.Position;
			model.Register(new Trigger<UnitMovePostEvent>(UnitMoved));
		}

		public abstract char Symbol { get; }

		public Entity Model
		{
			get { return model; }
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