using System;
using System.Collections.Generic;
using GooseEngine;
using GooseEngineView.Console.EntityViews;
using GooseEngine.Entities;
using GooseEngine.Entities.Units;
using GooseEngineView.Console;

namespace GooseEngineView
{
	public class ViewFactory
	{
		private Dictionary<Type, Type> typeDict = new Dictionary<Type, Type> ();

		public ViewFactory ()
		{
			AddTypeLink<Agent, ConsoleAgentView> ();
			AddTypeLink<Wall,ConsoleWallView> ();
			AddTypeLink<Player,ConsolePlayerView> ();
			AddTypeLink<ImpassableWall, ConsoleImpassableWallView> ();
		}

		public void AddTypeLink<TModel, TView> () 
			where TModel : Entity
			where TView : ConsoleEntityView
		{
			typeDict.Add (typeof(TModel), typeof(TView));
		}

		public ConsoleEntityView ConstructEntityView (Entity model)
		{
			ConsoleEntityView retval = (ConsoleEntityView) Activator.CreateInstance (typeDict [model.GetType ()]);
			retval.Model = model;
			return retval;
		}
	}
}