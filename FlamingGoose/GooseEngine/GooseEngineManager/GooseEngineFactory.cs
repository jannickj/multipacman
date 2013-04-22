using System.Threading;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngineController;
using GooseEngineView.Testing.ConsoleView;

namespace GooseEngineManager
{
	public class GooseEngineFactory
	{
		public virtual GooseModel ConstructEngine(GooseMap map)
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

		private EventManager ConstructEventManager()
		{
			return new EventManager();
		}

		private ActionManager ConstructActionManager()
		{
			return new ActionManager();
		}


		public GooseConsoleView ConstructView(ConsoleWorldView view)
		{
			return new GooseConsoleView(view);
		}

		public void Start(GooseModel model, GooseConsoleView view, GooseController controller)
		{
			GooseFactory fact = model.Factory;
			Thread modelt = fact.CreateThread(model.Start);
			Thread cont = fact.CreateThread(controller.Start);

			view.Setup();
			modelt.Start();
			cont.Start();
		}
	}
}