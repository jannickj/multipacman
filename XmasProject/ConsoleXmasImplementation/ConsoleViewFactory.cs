using System;
using ConsoleXmasImplementation.EntityViews;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineView;

namespace ConsoleXmasImplementation
{
	public class ConsoleViewFactory : ViewFactory
	{
		#region implemented abstract members of ViewFactory

		public ConsoleViewFactory()
		{
			AddTypeLink<Agent, ConsoleAgentView>();
			AddTypeLink<Wall, ConsoleWallView>();
			AddTypeLink<Player, ConsolePlayerView>();
			AddTypeLink<ImpassableWall, ConsoleImpassableWallView>();
		}

		public override EntityView ConstructEntityView(XmasEntity model)
		{
			ConsoleEntityView retval = (ConsoleEntityView) Activator.CreateInstance(typeDict[model.GetType()]);
			retval.Model = model;
			return retval;
		}

		#endregion
	}
}