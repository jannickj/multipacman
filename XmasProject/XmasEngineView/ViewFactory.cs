using System;
using System.Collections.Generic;
using XmasEngineModel.EntityLib;

namespace XmasEngineView
{
	public abstract class ViewFactory
	{
		protected Dictionary<Type, Type> typeDict = new Dictionary<Type, Type>();

		public void AddTypeLink<TModel, TView>()
			where TModel : XmasEntity
			where TView : EntityView
		{
			typeDict.Add(typeof (TModel), typeof (TView));
		}

		public abstract EntityView ConstructEntityView(XmasEntity model);
	}
}