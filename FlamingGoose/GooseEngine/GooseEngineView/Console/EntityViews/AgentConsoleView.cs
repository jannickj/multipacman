using GooseEngine;

namespace GooseEngineView.Console.EntityViews
{
	public class AgentConsoleView : ConsoleEntityView
	{
		public AgentConsoleView(Entity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'A'; }
		}
	}
}