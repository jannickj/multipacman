using System;
using System.Collections.Generic;
using XmasEngineModel.Management;

namespace XmasEngineModel.EntityLib.Module
{
	public abstract class EntityModule : XmasActor
	{
		private XmasEntity entityHost;
		private EntityModule replacedModule;

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
			if (ReplacedModule != null && ReplacedModule.ModuleType == this.ModuleType)
				entityHost.RegisterModule (ReplacedModule);
		}

		public override ActionManager ActionManager
		{
			get { return this.entityHost.ActionManager; }
		}

		public override EventManager EventManager
		{
			get { return this.entityHost.EventManager; }
		}

		public override XmasFactory Factory
		{
			get { return this.entityHost.Factory; }
		}

		public override XmasWorld World
		{
			get { return this.entityHost.World; }
		}

		protected EntityModule ReplacedModule
		{
			get { return replacedModule; }
		}
	}
}