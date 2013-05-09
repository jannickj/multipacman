using ConsoleXmasImplementation.Control;
using XmasEngine;
using XmasEngineController;
using XmasEngineModel;

namespace ConsoleXmasImplementation
{
    public class ConsoleFactory : XmasEngineFactory<ConsoleView,XmasController>
    {

        public override ConsoleView ConstructView(XmasModel model)
        {
			//TODO: FIX view construction
	        return null; //new ConsoleView(model,contructWorldView(model.World));
        }

        private ConsoleWorldView contructWorldView(XmasWorld modelworld)
        {
            return new ConsoleWorldView(modelworld);
        }

        public override XmasController ContructController(XmasModel model, ConsoleView view)
        {
            return new ConsoleController(model, view);
        }
    }
}
