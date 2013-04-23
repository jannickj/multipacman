using System;
using GooseEngine;
using GooseEngineView.Console;

namespace GooseEngineView
{
	public class PlayerConsoleView : ConsoleEntityView
	{
		public PlayerConsoleView(Entity model)
			: base(model)
		{
		}
		
		public override char Symbol
		{
			get { return 'P'; }
		}
	}
}

