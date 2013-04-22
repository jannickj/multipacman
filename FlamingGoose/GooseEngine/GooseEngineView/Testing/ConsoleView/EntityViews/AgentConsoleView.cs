using GooseEngine;

namespace GooseEngineView.Testing.ConsoleView.EntityViews
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