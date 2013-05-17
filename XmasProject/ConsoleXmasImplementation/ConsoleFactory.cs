using ConsoleXmasImplementation.Controller;
using ConsoleXmasImplementation.View;
using XmasEngine;
using XmasEngineController;
using XmasEngineExtensions.EisExtension.Controller.AI;
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
			ConsoleController con = new ConsoleController(model, (ConsoleView) view);

			return con;
		}
	}
}