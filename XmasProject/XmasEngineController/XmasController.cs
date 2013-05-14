using System.Collections.Generic;
using System.Threading;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineView;

namespace XmasEngineController
{
	public abstract class XmasController
	{
		private List<AgentManager> aiservs = new List<AgentManager>();
		private XmasModel model;
		private XmasView view;

		public XmasController(XmasModel model, XmasView view)
		{
			this.model = model;
			this.view = view;
		}

		public void AddAiServer(AgentManager manager)
		{
			model.AddActor(manager);
			aiservs.Add(manager);
		}

		public virtual void Initialize()
		{
			foreach (AgentManager serv in aiservs)
				serv.Initialize();
		}

		public virtual void Start()
		{
			foreach (AgentManager ac in aiservs)
			{
				Thread t = model.Factory.CreateThread(ac.Start);
				t.Name = ac.GetType().Name + " thread";
				t.Start();
			}
		}
	}
}