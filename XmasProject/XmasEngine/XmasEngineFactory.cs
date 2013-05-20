using System;
using XmasEngineController;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
	public abstract class XmasEngineFactory
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

		public abstract XmasView ConstructView(XmasModel model);


		public abstract XmasController ContructController(XmasModel model, XmasView view);


		public Tuple<XmasModel, XmasView, XmasController> FullConstruct(XmasWorldBuilder builder,
		                                                                params AgentManager[] agentmanagers)
		{
			XmasModel model = ConstructModel(builder);
			XmasView view = ConstructView(model);
			XmasController controller = ContructController(model, view);

			foreach (AgentManager aman in agentmanagers)
				controller.AddAiServer(aman);


			return Tuple.Create(model, view, controller);
		}
	}
}