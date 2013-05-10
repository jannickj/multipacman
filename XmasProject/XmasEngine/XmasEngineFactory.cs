using System;
using System.Threading;
using XmasEngineController;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
	public abstract class XmasEngineFactory
	{
		public virtual XmasModel ConstructModel(XmasWorldBuilder builder)
		{

			//TODO: FIX Factory code
			XmasWorld world = ConstructWorld(builder);
			ActionManager actman = ConstructActionManager();
			EventManager evtman = ConstructEventManager();
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

		protected virtual ActionManager ConstructActionManager()
		{
			return new ActionManager();
		}

		public abstract XmasView ConstructView(XmasModel model);

		public abstract XmasWorld ConstructWorld(XmasWorldBuilder builder);


        public abstract XmasController ContructController(XmasModel model, XmasView view);


        
		public Tuple<XmasModel,XmasView,XmasController> FullConstruct(XmasWorldBuilder builder,params AgentFactory[] agentFactory)
		{
			XmasModel model = this.ConstructModel(builder);
            XmasView view = this.ConstructView(model);
            XmasController controller = this.ContructController(model, view);

            foreach (AgentFactory afact in agentFactory)
                controller.AddAiServer(afact.ContructServer());
           

			return Tuple.Create(model, view, controller);
		}

		
	}
}