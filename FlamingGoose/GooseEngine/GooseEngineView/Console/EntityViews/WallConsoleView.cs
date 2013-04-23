using GooseEngine;

namespace GooseEngineView.Console.EntityViews
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