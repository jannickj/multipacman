using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GooseEngine;
using GooseEngine.Conversion;
using GooseEngine.EIS;
using GooseEngine.GameManagement;
using GooseEngine.Lib;
using GooseEngineController.AI;
using GooseEngineController.EIS.AI;
using GooseEngineView.Testing.ConsoleView;

namespace GooseEngineManager
{
    public class GooseEngineFactory
    {
        public AgentServer ConstructAgentServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"),13337);
            EISAgentServer server = new EISAgentServer(listener, ContructEISConversionTool(), ConstructIILActionParser());
            return server;
        }

        private IILActionParser ConstructIILActionParser()
        {
            IILActionParser actparser = new IILActionParser();
            return actparser;
        }

        private EISConversionTool ContructEISConversionTool()
        {
            EISConversionTool tool = new EISConversionTool();
            List<Type> converters = ExtendedType.FindAllDerivedTypes<GooseConverter>();
            foreach (Type t in converters.Where(t => !t.IsAbstract))
            {
                tool.AddConverter((GooseConverter<GooseObject,iilang.IILElement>)Activator.CreateInstance(t));
            }
            return tool;
        }

        public virtual GooseModel ConstructEngine(GooseMap map)
        {
            GooseWorld world = new GooseWorld(map);
            ActionManager actman = ConstructActionManager();
            EventManager evtman = ConstructEventManager();
            GameFactory fact = ConstructGameFactory(actman);
            GooseModel engine = new GooseModel(world,actman,evtman,fact);

            return engine;
        }

        protected virtual GameFactory ConstructGameFactory(ActionManager actman)
        {
            return new GameFactory(actman);
        }

        private EventManager ConstructEventManager()
        {
            return new EventManager();
        }

        private ActionManager ConstructActionManager()
        {
            return new ActionManager();
        }


        public  GooseConsoleView ConstructView(ConsoleWorldView view)
        {
            return new GooseConsoleView(view);
        }
    }
}
