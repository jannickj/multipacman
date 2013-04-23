using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using GooseEISExtension.Controller.AI;
using GooseEISExtension.Model;
using GooseEngine;
using GooseEngine.Conversion;
using JSLibrary;
using iilang;
using GooseEngineController;
using GooseEngineController.AI;

namespace GooseEISExtension
{
    public class EisAgentFactory : AgentFactory
	{
        private IPAddress ip;
        private int port;

        public EisAgentFactory(IPAddress ip, int port)
        {
            this.ip = ip;
            this.port = port;
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
				tool.AddConverter((GooseConverter<GooseObject, IILElement>) Activator.CreateInstance(t));
			}
			return tool;
		}



        public override AgentServer ContructServer()
        {
            TcpListener listener = new TcpListener(ip, port);
            EISAgentServer server = new EISAgentServer(listener, ContructEISConversionTool(), ConstructIILActionParser());
            return server;
        }


    }
}