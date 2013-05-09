using System;
using ConsoleXmasImplementation.EntityViews;
using XmasEngineModel;
using XmasEngineModel.Entities;
using XmasEngineModel.Entities.Units;
using XmasEngineView;

namespace ConsoleXmasImplementation
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

		public override EntityView ConstructEntityView (Entity model)
		{
			ConsoleEntityView retval = (ConsoleEntityView) Activator.CreateInstance (typeDict [model.GetType ()]);
			retval.Model = model;
			return retval;
		}

		#endregion



	}
}