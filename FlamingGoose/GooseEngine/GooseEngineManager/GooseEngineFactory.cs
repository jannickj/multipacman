using System;
using System.Collections.Generic;
using System.Threading;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngineController;
using GooseEngineController.AI;
using GooseEngineView;

namespace GooseEngineManager
{
	public abstract class GooseEngineFactory<TView,TController>
		where TView : GooseView
		where TController : GooseController
	{
		public virtual GooseModel ConstructModel(GooseMap map)
		{
			GooseWorld world = new GooseWorld(map);
			ActionManager actman = ConstructActionManager();
			EventManager evtman = ConstructEventManager();
			GooseFactory fact = ConstructGameFactory(actman);
			GooseModel engine = new GooseModel(world, actman, evtman, fact);

			return engine;
		}

		protected virtual GooseFactory ConstructGameFactory(ActionManager actman)
		{
			return new GooseFactory(actman);
		}

		protected virtual EventManager ConstructEventManager()
		{
			return new EventManager();
		}

		protected virtual ActionManager ConstructActionManager()
		{
			return new ActionManager();
		}

		public abstract TView ConstructView(GooseModel model);


        public abstract TController ContructController(GooseModel model, TView view);


        
		public Tuple<GooseModel,TView,TController> FullConstruct(GooseMap map,params AgentFactory[] agentFactory)
		{
			GooseModel model = this.ConstructModel(map);
            TView view = this.ConstructView(model);
            TController controller = this.ContructController(model, view);

            foreach (AgentFactory afact in agentFactory)
                controller.AddAiServer(afact.ContructServer());
           

			return Tuple.Create(model, view, controller);
		}

		public void StartEngine(GooseModel model, GooseView view, GooseController controller)
		{
			GooseFactory fact = model.Factory;
			Thread modelt = fact.CreateThread(model.Start);
			Thread viewt = fact.CreateThread(view.Start);
			Thread cont = fact.CreateThread(controller.Start);

			model.Initialize();
			view.Initialize();
			controller.Initialize();

			modelt.Start();
			viewt.Start();
			cont.Start();
		}
	}
}