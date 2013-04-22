using GooseEngine;

namespace GooseEngineView.Testing.ConsoleView.EntityViews
{
	public class WallConsoleView : ConsoleEntityView
	{
		public WallConsoleView(Entity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'W'; }
		}
	}
}