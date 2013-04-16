using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GooseEngine;
using GooseEngineView.Testing.ConsoleView;

namespace EpicConsoleGame
{
    class Program
    {
        

        static void Main(string[] args)
        {
            EpicFactory factory = new EpicFactory();
            GameEngine engine = factory.ConstrucEngine();

            Thread thread = new Thread(new ThreadStart(engine.Start));

            GooseConsoleView view = factory.ConstrucView();


        }
    }
}
