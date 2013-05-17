using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Rule;
using XmasEngineModel.World;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineModel.EntityLib
{
	public abstract class XmasEntity : XmasActor
	{
		private TriggerManager triggers = new TriggerManager();
		internal Dictionary<Type, EntityModule> moduleMap = new Dictionary<Type, EntityModule>();

		public XmasEntity()
		{
		}

		public TModule Module<TModule> ()
			where TModule : EntityModule
		{
			try {
				return moduleMap [typeof(TModule)] as TModule;
			} catch (KeyNotFoundException e) {
				//TODO: raise ModuleNotFoundException or something like that
				throw new NotImplementedException();
			}	
		}

		public virtual EntityModule RegisterModule(EntityModule module)
		{
			EntityModule oldModule;
			module.EntityHost = this;

			moduleMap.TryGetValue (module.ModuleType, out oldModule);
			moduleMap [module.ModuleType] = module;

			module.AttachToEntity (this, oldModule);

			return oldModule;
		}

		public virtual void DeregisterModule(EntityModule module)
		{
			moduleMap.Remove (module.ModuleType);
			module.DetachFromEntity ();
		}

		public XmasPosition Position
		{
			get { return World.GetEntityPosition(this); }
		}

		internal event EventHandler<XmasEvent> TriggerRaised;

		public void Register(Trigger trigger)
		{
			lock (this)
			{
				triggers.Register(trigger);
			}
		}

		public void Deregister(Trigger trigger)
		{
			lock (this)
			{
				triggers.Deregister(trigger);
			}
		}

		public void QueueAction(EntityXmasAction action)
		{
			action.Source = this;
			ActionManager.QueueAction(action);
		}

		public void Raise(XmasEvent evt)
		{
			lock (this)
			{
				triggers.Raise(evt);
				if (TriggerRaised != null)
					TriggerRaised(this, evt);
			}
		}

		public ThreadSafeEventQueue ConstructEventQueue()
		{
			return new ThreadSafeEventQueue(triggers);
		}
	}
}