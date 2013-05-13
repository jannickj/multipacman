using ConsoleXmasImplementation.Controller;
using ConsoleXmasImplementation.View;
using XmasEngine;
using XmasEngineController;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel;
using XmasEngineView;

namespace ConsoleXmasImplementation
{
	public class ConsoleFactory : XmasEngineFactory
	{
		public override XmasView ConstructView(XmasModel model)
		{
			return new ConsoleView(model, new ConsoleWorldView((TileWorld) model.World), new ConsoleViewFactory());
		}


		public override XmasController ContructController(XmasModel model, XmasView view)
		{
			return new ConsoleController(model, (ConsoleView) view);
		}
	}
}