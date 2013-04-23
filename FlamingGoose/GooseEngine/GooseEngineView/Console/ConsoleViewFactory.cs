using GooseEngineView;
using System;
using GooseEngine.Entities.Units;
using GooseEngine.Entities;
using GooseEngineView.Console.EntityViews;

namespace GooseEngineView.Console
{
	public class ConsoleViewFactory : ViewFactory
	{
		#region implemented abstract members of ViewFactory

		public ConsoleViewFactory()
		{
			AddTypeLink<Agent, ConsoleAgentView> ();
			AddTypeLink<Wall, ConsoleWallView> ();
			AddTypeLink<Player, ConsolePlayerView> ();
			AddTypeLink<ImpassableWall, ConsoleImpassableWallView> ();
		}

		public override EntityView ConstructEntityView (GooseEngine.Entity model)
		{
			ConsoleEntityView retval = (ConsoleEntityView) Activator.CreateInstance (typeDict [model.GetType ()]);
			retval.Model = model;
			return retval;
		}

		#endregion



	}
}