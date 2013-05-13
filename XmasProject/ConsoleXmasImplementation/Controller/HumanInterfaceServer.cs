using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using XmasEngineController.AI;

namespace ConsoleXmasImplementation.Controller
{
	public class HumanInterfaceServer : AgentServer
	{
		public HumanInterfaceServer()
		{

		}

		public override void Initialize()
		{
			
		}

		protected override Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor()
		{
			return agentControllerConstructor;
		}

		private KeyValuePair<string, AgentController> agentControllerConstructor()
		{

			throw new NotImplementedException();
		}
	}
}
