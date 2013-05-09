using XmasEngineController;
using XmasEngineController.Console;
using XmasEngineModel;
using XmasEngineView.Console;

namespace XmasEngine
{
    public class ConsoleXmasImplFactory : XmasEngineFactory<XmasConsoleView,XmasController>
    {

        public override XmasConsoleView ConstructView(XmasModel model)
        {
			//TODO: FIX view construction
	        return null; //new XmasConsoleView(model,contructWorldView(model.World));
        }

        private ConsoleWorldView contructWorldView(TileWorld modelworld)
        {
            return new ConsoleWorldView(modelworld);
        }

        public override XmasController ContructController(XmasModel model, XmasConsoleView view)
        {
            return new ConsoleController(model, view);
        }
    }
}
