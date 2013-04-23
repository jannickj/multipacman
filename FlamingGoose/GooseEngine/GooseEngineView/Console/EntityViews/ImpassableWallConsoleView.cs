using System;
using GooseEngineView.Console;
using GooseEngine;

public class ImpassableWallConsoleView : ConsoleEntityView
{
	public ImpassableWallConsoleView(Entity model)
		: base(model)
	{
	}
	
	public override char Symbol
	{
		get { return 'I'; }
	}
}

