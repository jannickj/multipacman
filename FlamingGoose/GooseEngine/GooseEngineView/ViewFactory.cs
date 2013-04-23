using System;
using System.Collections.Generic;
using GooseEngine;
using GooseEngineView.Console.EntityViews;
using GooseEngine.Entities;
using GooseEngine.Entities.Units;
using GooseEngineView.Console;

namespace GooseEngineView
{
	public abstract class ViewFactory
	{
		protected Dictionary<Type, Type> typeDict = new Dictionary<Type, Type> ();

		public void AddTypeLink<TModel, TView> () 
			where TModel : Entity
			where TView : ConsoleEntityView
		{
			typeDict.Add (typeof(TModel), typeof(TView));
		}

		public abstract EntityView ConstructEntityView (Entity model);
	}
}