using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GooseEngine;
using GooseEngine.AI;
using GooseEngine.Conversion;
using GooseEngine.EIS;
using GooseEngine.GameManagement;
using GooseEngine.Lib;
using GooseEngineView.Testing.ConsoleView;

namespace EpicConsoleGame
{
    public class EpicFactory
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

        public virtual GameEngine ConstrucEngine(GameMap map)
        {
            GameWorld world = new GameWorld(map);
            ActionManager actman = ConstrucActionManager();
            EventManager evtman = ConstrucEventManager();
            GameFactory fact = ConstrucGameFactory(actman);
            GameEngine engine = new GameEngine(world,actman,evtman,fact);

            return engine;
        }

        protected virtual GameFactory ConstrucGameFactory(ActionManager actman)
        {
            return new GameFactory(actman);
        }

        private EventManager ConstrucEventManager()
        {
            return new EventManager();
        }

        private ActionManager ConstrucActionManager()
        {
            return new ActionManager();
        }


        public  GooseConsoleView ConstrucView(GameEngine engine)
        {
            return new GooseConsoleView(engine.World);
        }
    }
}
