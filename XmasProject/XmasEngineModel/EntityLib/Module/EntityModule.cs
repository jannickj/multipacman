using System;
using System.Collections.Generic;

namespace XmasEngineModel.EntityLib.Module
{
	public abstract class EntityModule
	{
		protected XmasEntity entityHost;
		protected EntityModule replacedModule;

		public XmasEntity EntityHost
		{
			get { return entityHost; }
			internal set { entityHost = value; }
		}

		public virtual Type ModuleType { 
			get { return this.GetType (); } 
		}

		public virtual IEnumerable<Percept> Percepts
		{
			get { return new Percept[0]; }
		}

		public virtual void AttachToEntity(XmasEntity entityHost, EntityModule replacedModule)
		{
			this.entityHost = entityHost;

			if (replacedModule != null && replacedModule.ModuleType == this.ModuleType)
				this.replacedModule = replacedModule;
		}

		public virtual void DetachFromEntity()
		{
			if (replacedModule != null && replacedModule.ModuleType == this.ModuleType)
				entityHost.RegisterModule (replacedModule);
		}
	}
}