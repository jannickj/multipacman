using System.Threading;
using GooseEISExtension;
using GooseEngine;
using GooseEngineController;
using GooseEngineManager;
using GooseEngineView;
using GooseEngineView.Testing.ConsoleView;

namespace EpicConsoleGame
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			GooseEngineFactory<GooseModel,GooseView,GooseController> factory = null;
			EisGooseEngineFactory eisfactory = new EisGooseEngineFactory();

			GooseModel model = factory.ConstructModel(new SweetMap());
			
			GooseConsoleView view = factory.ConstructView(model);

			GooseController controller = factory.ContructController(model, view);

			factory.StartEngine(model, view, controller);
		}
	}
}