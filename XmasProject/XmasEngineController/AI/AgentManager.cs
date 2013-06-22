using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using JSLibrary;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Interfaces;

namespace XmasEngineController.AI
{
	public abstract class AgentManager : XmasController, IStartable
	{
		

		public AgentManager()
		{
		
		}


		public override void Start()
		{
			while (true)
			{
				Func<KeyValuePair<string, AgentController>> agentcontroller = AquireAgentControllerContructor();
				Thread thread = null;
				thread = Factory.CreateThread(() => agent_Thread(agentcontroller));
				thread.Start();
			}
		}


		protected abstract Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor();


		private void agent_Thread(Func<KeyValuePair<string, AgentController>> constructor)
		{
			KeyValuePair<string, AgentController> agent;
			try
			{
				if (this.AgentControllerConstructionTimeOut == 0)
				{
					agent = constructor();
				}
				else
					Parallel.TryExecute(constructor, this.AgentControllerConstructionTimeOut, out agent);

				Thread.CurrentThread.Name = agent.Key+" Thread";
				
				agent.Value.Start();
			}
			catch (TimeoutException)
			{
				throw new TimeoutException("Agent Controller construction timed out");
			}
		}

		public Agent TakeControlOf(string name)
		{
			lock (this)
			{
				Agent agent;
				if (this.World.TryGetAgent(name, out agent))
				{
					return agent;
				}
				else
					throw new Exception("Agent controller was unable to assume control of " + name);
			}
		}

		public virtual int AgentControllerConstructionTimeOut
		{
			get
			{
				return 0;
			}
		}
		

		

		
	}
}