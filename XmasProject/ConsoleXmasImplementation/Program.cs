using System.Net;
using XmasEngine;
using XmasEngineExtensions.EisExtension;

namespace ConsoleXmasImplementation
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ConsoleFactory factory = new ConsoleFactory();

			var t = factory.FullConstruct(new TestWorld1(),new EisAgentFactory(IPAddress.Parse("127.0.0.1"),33766));

			var engine = new XmasEngineManager(factory);
		}
	}
}