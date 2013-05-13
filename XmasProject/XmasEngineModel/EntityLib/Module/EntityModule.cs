using System;

namespace XmasEngineModel.EntityLib.Module
{
	public abstract class EntityModule
	{
		private XmasEntity xmasEntity;

		public XmasEntity XmasEntity
		{
			get { return xmasEntity; }
			internal set { xmasEntity = value; }
		}

		public virtual Type ModuleType
		{ get { return this.GetType();  } }
	
	}
}