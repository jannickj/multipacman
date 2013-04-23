using GooseEngine;

namespace GooseEngineView.Console.EntityViews
{
	public class ConsoleAgentView : ConsoleEntityView
	{
		public ConsoleAgentView(Entity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'A'; }
		}
	}
}