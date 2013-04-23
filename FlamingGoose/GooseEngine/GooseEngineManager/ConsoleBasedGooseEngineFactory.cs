using GooseEngine;
using GooseEngineController;
using GooseEngineController.Console;
using GooseEngineView.Testing.ConsoleView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngineManager
{
    public class ConsoleGooseImplFactory : GooseEngineFactory<GooseConsoleView,GooseController>
    {

        public override GooseConsoleView ConstructView(GooseModel model)
        {
            return new GooseConsoleView(contructWorldView(model.World));
        }

        private ConsoleWorldView contructWorldView(GooseWorld modelworld)
        {
            return new ConsoleWorldView(modelworld);
        }

        public override GooseController ContructController(GooseModel model, GooseConsoleView view)
        {
            return new ConsoleController(model, view);
        }
    }
}
