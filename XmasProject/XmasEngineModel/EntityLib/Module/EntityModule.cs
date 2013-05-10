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

		public abstract Type ModuleType { get; }
	}
}