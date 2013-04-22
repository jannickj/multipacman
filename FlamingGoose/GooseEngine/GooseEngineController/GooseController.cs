using System.Collections.Generic;
using System.Threading;
using GooseEngine;
using GooseEngineController.AI;

namespace GooseEngineController
{
	public class GooseController
	{
		private List<AgentServer> aiservs = new List<AgentServer>();
		private GooseModel model;

		public GooseController(GooseModel model)
		{
			this.model = model;
		}

		public void AddAiServer(AgentServer server)
		{
			aiservs.Add(server);
		}

		public void Start()
		{
			foreach (AgentServer ac in aiservs)
			{
				Thread t = model.Factory.CreateThread(ac.Start);
				t.Start();
			}
		}
	}
}