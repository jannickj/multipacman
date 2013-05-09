using System.Collections.Generic;
using System.Threading;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineView;

namespace XmasEngineController
{
	public abstract class XmasController
	{
		private List<AgentServer> aiservs = new List<AgentServer>();
		private XmasModel model;
        private XmasView view;

        public XmasController(XmasModel model, XmasView view)
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