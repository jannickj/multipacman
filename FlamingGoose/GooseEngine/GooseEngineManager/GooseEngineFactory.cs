using System;
using System.Collections.Generic;
using System.Threading;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngineController;
using GooseEngineController.AI;
using GooseEngineView;
using GooseEngineView.Testing.ConsoleView;

namespace GooseEngineManager
{
	public class GooseEngineFactory<TModel,TView,TController>
		where TModel : GooseModel
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

		private ConsoleWorldView ConstructWorldView(GooseWorld world)
		{
			return new ConsoleWorldView(world);
		}

		public virtual GooseConsoleView ConstructView(GooseModel model)
		{

			return new GooseConsoleView(ConstructWorldView(model.World));
		}

		public virtual GooseController ContructController(GooseModel model, GooseView view)
		{
			return new GooseController(model);
		}

		public Tuple<GooseModel,GooseView,GooseWorld> SimpleConstructAndStartOfEngine(GooseMap map, List<AgentServer> agentServers)
		{
			GooseModel model = this.ConstructModel(map);
			
		
			return null;
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