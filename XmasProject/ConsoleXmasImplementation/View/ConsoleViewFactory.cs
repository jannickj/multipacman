using System;
using ConsoleXmasImplementation.View.EntityViews;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineModel.EntityLib;
using XmasEngineView;

namespace ConsoleXmasImplementation.View
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