using System;
using XmasEngineController;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
	public class XmasModelFactory
	{
		public virtual XmasModel ConstructModel(XmasWorldBuilder builder)
		{
			EventManager evtman = ConstructEventManager();
			ActionManager actman = ConstructActionManager(evtman); 
			XmasWorld world = builder.Build(actman);
			XmasFactory fact = ConstructGameFactory(actman);
			XmasModel engine = new XmasModel(world, actman, evtman, fact);

			return engine;
		}


		protected virtual XmasFactory ConstructGameFactory(ActionManager actman)
		{
			return new XmasFactory(actman);
		}

		protected virtual EventManager ConstructEventManager()
		{
			return new EventManager();
		}

		protected virtual ActionManager ConstructActionManager(EventManager evtman)
		{
			return new ActionManager(evtman);
		}

	}
}