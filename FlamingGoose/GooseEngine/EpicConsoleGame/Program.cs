using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine;
using GooseEngineManager;
using GooseEngineView.Testing.ConsoleView;

namespace EpicConsoleGame
{
    class Program
    {
        

        static void Main(string[] args)
        {
            GooseEngineFactory factory = new GooseEngineFactory();
            GooseModel engine = factory.ConstructEngine(new SweetMap());

            Thread thread = new Thread(new ThreadStart(engine.Start));

            GooseConsoleView view = factory.ConstructView(null);


        }
    }
}
