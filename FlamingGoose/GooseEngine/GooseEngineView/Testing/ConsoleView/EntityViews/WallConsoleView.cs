using System;
using GooseEngineView.Testing.ConsoleView;
using GooseEngine;

namespace GooseEngineView.Testing.ConsoleView.EntityViews
{
	public class WallConsoleView : ConsoleEntityView
	{
		public char Symbol { get { return "W"; } }

		public WallConsoleView (Entity model) 
			: base(model)
		{
		}
	}
}

