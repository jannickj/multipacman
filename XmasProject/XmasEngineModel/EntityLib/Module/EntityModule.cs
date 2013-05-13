using System;
using System.Collections.Generic;

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

		public virtual Type ModuleType { 
			get { return this.GetType (); } 
		}
	}

	public class ModuleEqualityComparer : IEqualityComparer<EntityModule>
	{
		#region IEqualityComparer implementation

		public bool Equals (EntityModule x, EntityModule y)
		{
			return x.ModuleType.Equals (y.ModuleType);
		}

		public int GetHashCode (EntityModule obj)
		{
			return obj.ModuleType.GetHashCode ();
		}

		#endregion
	}
}