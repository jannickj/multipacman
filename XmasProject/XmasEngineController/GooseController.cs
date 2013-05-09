using System.Collections.Generic;
using System.Threading;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineView.Console;

namespace XmasEngineController
{
	public abstract class GooseController
	{
		private List<AgentServer> aiservs = new List<AgentServer>();
		private GooseModel model;
        private GooseConsoleView view;

        public GooseController(GooseModel model, GooseConsoleView view)
        {
            this.model = model;
            this.view = view;
        }

		public void AddAiServer(AgentServer server)
		{
            model.AddActor(server);
			aiservs.Add(server);
		}

		public virtual void Initialize()
		{
            foreach (AgentServer serv in aiservs)
                serv.Initialize();
		}

		public void Start()
		{
			foreach (AgentServer ac in aiservs)
			{
				Thread t = model.Factory.CreateThread(ac.Start);
				t.Name = ac.GetType().Name + " thread";
				t.Start();
			}
		}
	}
}