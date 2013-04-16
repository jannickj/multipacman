using System;
using GooseEngineView.Testing.ConsoleView;
using GooseEngine;

namespace GooseEngineView.Testing.ConsoleView.EntityViews
{
	public class AgentConsoleView : ConsoleEntityView
	{
		public char Symbol { get { return "A"; } }

		public AgentConsoleView(Entity model) 
			: base(model)
		{
		}
	}
}

