using System;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management
{
	public abstract class EntityXmasAction : XmasAction
	{
		private XmasEntity source;

		public XmasEntity Source
		{
			get { return source; }
			internal set { source = value; }
		}
	
	}

	public abstract class EntityXmasAction<T> : EntityXmasAction where T : XmasEntity
	{
		public new T Source
		{
			get { return (T) base.Source; }
		}


	}
}