using System;
using GooseEngine;
using GooseEngineView.Console;

namespace GooseEngineView
{
	public class ConsolePlayerView : ConsoleEntityView
	{
		public ConsolePlayerView(Entity model)
			: base(model)
		{
		}
		
		public override char Symbol
		{
			get { return 'P'; }
		}
	}
}

