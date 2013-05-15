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

		public virtual ICollection<Percept> Percepts
		{
			get { return new Percept[0]; }
		}
	}
}