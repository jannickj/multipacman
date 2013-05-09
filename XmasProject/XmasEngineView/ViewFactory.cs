using System;
using System.Collections.Generic;
using XmasEngineModel;
using XmasEngineView.Console;

namespace XmasEngineView
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