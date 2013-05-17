using System;
using System.Threading;
using ConsoleXmasImplementation.View;
using XmasEngineController;
using XmasEngineModel;

namespace ConsoleXmasImplementation.Controller
{
	public class ConsoleController : XmasController
	{
		public ConsoleController(XmasModel model, ConsoleView view) : base(model, view)
		{

		}

		public override void Start()
		{
			base.Start();
			while (true)
			{
				if (Console.KeyAvailable)
				{
					var key = Console.ReadKey(true);
					Console.WriteLine("													"+key.KeyChar);
					
				}
				Thread.Sleep(200);
			}
		}

		
 
	}
}