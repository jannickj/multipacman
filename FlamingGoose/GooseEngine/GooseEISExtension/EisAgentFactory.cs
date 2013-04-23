using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using GooseEISExtension.Controller.AI;
using GooseEISExtension.Model;
using GooseEISExtension.Model.Conversion.IILang;
using GooseEngine;
using GooseEngine.Conversion;
using JSLibrary;
using iilang;
using GooseEngineController;
using GooseEngineController.AI;
using System.Reflection;

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

			MethodInfo gmethod = typeof(GooseConversionTool<>).GetMethod("AddConverter");

			foreach (Type t in converters.Where(t => t.BaseType != null && t.BaseType.IsGenericType && !t.IsAbstract))
			{
				Type actionType = typeof (EISActionConverter<,>);
				Type perceptType = typeof (EISPerceptConverter<>);
				Type EISconvert = typeof (EISConverterToEIS<,>);

				Type basetype = t.BaseType.GetGenericTypeDefinition();
				
				if (basetype == actionType || basetype == perceptType || basetype == EISconvert)
				{
					MethodInfo typedmethod = gmethod.MakeGenericMethod(t.GetGenericArguments());
					typedmethod.Invoke(tool,new object[]{Activator.CreateInstance(t)});
				}

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