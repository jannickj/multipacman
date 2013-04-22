using System.Threading;
using GooseEngine;
using GooseEngineManager;
using GooseEngineView.Testing.ConsoleView;

namespace EpicConsoleGame
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			GooseEngineFactory factory = new GooseEngineFactory();
			GooseModel engine = factory.ConstructEngine(new SweetMap());

			Thread thread = new Thread(engine.Start);

			GooseConsoleView view = factory.ConstructView(null);
		}
	}
}