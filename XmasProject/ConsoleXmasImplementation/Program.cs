using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using XmasEngine;
using XmasEngineController;
using XmasEngineExtensions.EisExtension;
using XmasEngineExtensions.EisExtension.Controller.AI;
using XmasEngineExtensions.LoggerExtension;
using XmasEngineExtensions.TileEisExtension;
using XmasEngineView;

namespace ConsoleXmasImplementation
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ConsoleFactory factory = new ConsoleFactory();

			var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 44444);
			var EisConverter = new TileEisConversionTool();
			var IilParser = new TileIilActionParser();
			var eisserver = new EISAgentServer(listener,EisConverter, IilParser);
			


			var t = factory.FullConstruct(new TestWorld1(),eisserver);

			StreamWriter sw = File.CreateText("error.log");

			List<XmasView> views = new List<XmasView>();

			views.Add(new LoggerView(t.Item1,sw));
			views.Add(t.Item2);
			
			List<XmasController> controllers = new List<XmasController>();

			controllers.Add(t.Item3);

			var engine = new XmasEngineManager(factory);

			engine.StartEngine(t.Item1,views,controllers);
		}
	}
}