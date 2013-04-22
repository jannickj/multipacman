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

namespace GooseEISExtension
{
	public class EisGooseEngineFactory
	{
		public EISAgentServer ConstructEisAgentServer(string ip, int port)
		{
			TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
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
				tool.AddConverter((GooseConverter<GooseObject, IILElement>) Activator.CreateInstance(t));
			}
			return tool;
		}
	}
}