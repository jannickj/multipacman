using System.Net;
using System.Net.Sockets;
using XmasEngine;
using XmasEngineExtensions.EisExtension;
using XmasEngineExtensions.EisExtension.Controller.AI;
using XmasEngineExtensions.TileEisExtension;

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

			var engine = new XmasEngineManager(factory);

			engine.StartEngine(t.Item1,t.Item2,t.Item3);
		}
	}
}