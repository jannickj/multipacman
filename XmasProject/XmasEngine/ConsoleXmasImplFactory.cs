using XmasEngineController;
using XmasEngineController.Console;
using XmasEngineModel;
using XmasEngineView.Console;

namespace XmasEngine
{
    public class ConsoleXmasImplFactory : GooseEngineFactory<XmasConsoleView,XmasController>
    {

        public override XmasConsoleView ConstructView(GooseModel model)
        {
			//TODO: FIX view construction
	        return null; //new GooseConsoleView(model,contructWorldView(model.World));
        }

        private ConsoleWorldView contructWorldView(GooseWorld modelworld)
        {
            return new ConsoleWorldView(modelworld);
        }

        public override XmasController ContructController(GooseModel model, XmasConsoleView view)
        {
            return new ConsoleController(model, view);
        }
    }
}
