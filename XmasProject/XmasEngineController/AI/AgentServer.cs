using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Interfaces;

namespace XmasEngineController.AI
{
	public abstract class AgentServer : XmasActor, IStartable
	{
		private Dictionary<AgentController, AgentControllerInfomation> agents =
			new Dictionary<AgentController, AgentControllerInfomation>();

		private HashSet<Agent> availableAgents = new HashSet<Agent>();
		private Dictionary<string, Agent> knownAgents = new Dictionary<string, Agent>();
		

		public AgentServer()
		{
		
		}


		public void Start()
		{
			while (true)
			{
				Func<KeyValuePair<string, AgentController>> agentcontroller = AquireAgentControllerContructor();
				Thread thread = null;
				thread = Factory.CreateThread(() => agent_Thread(agentcontroller));
				thread.Start();
			}
		}

		public abstract void Initialize();

		protected abstract Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor();


		private void agent_Thread(Func<KeyValuePair<string, AgentController>> constructor)
		{
			KeyValuePair<string, AgentController> agent;
			try
			{
				TryExecute(constructor, 2000, out agent);
				AgentControllerInfomation ainfo = new AgentControllerInfomation();
				ainfo.Thread = Thread.CurrentThread;
				ainfo.Name = agent.Key;
				lock (this)
					agents.Add(agent.Value, ainfo);

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
				if (knownAgents.TryGetValue(name, out agent))
				{
					availableAgents.Remove(agent);
					return agent;
				}
				else
					throw new Exception("Agent controller was unable to assume control of " + name);
			}
		}

		public void AddAgent(Agent agent)
		{
			lock (this)
			{
				knownAgents.Add(agent.Name, agent);
				availableAgents.Add(agent);
			}
		}

		public static T Execute<T>(Func<T> func, int timeout)
		{
			T result;
			TryExecute(func, timeout, out result);
			return result;
		}

		public static bool TryExecute<T>(Func<T> func, int timeout, out T result)
		{
			T t = default(T);
			Thread thread = new Thread(() => t = func());
			thread.Start();
			bool completed = thread.Join(timeout);
			if (!completed)
			{
				thread.Abort();
				throw new TimeoutException();
			}
			result = t;
			return completed;
		}

		private class AgentControllerInfomation
		{
			private String name;
			private Thread thread;

			public String Name
			{
				set
				{
					name = value;
					if (thread != null && thread.Name == null)
						thread.Name = ThreadName();
				}
			}

			public Thread Thread
			{
				set
				{
					thread = value;
					if (name != null && thread.Name == null)
						thread.Name = ThreadName();
				}
			}

			private string ThreadName()
			{
				return name + " Controller Thread";
			}
		}
	}
}