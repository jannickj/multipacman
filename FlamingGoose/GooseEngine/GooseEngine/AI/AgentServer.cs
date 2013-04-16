using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GooseEngine.Entities.Units;
using GooseEngine.Interfaces;

namespace GooseEngine.AI
{
    public abstract class AgentServer : GooseActor, IStartable
    {
        private TcpListener listener;
        private List<AgentController> agents = new List<AgentController>();
        private Dictionary<string, Agent> knownAgents = new Dictionary<string, Agent>();
        private HashSet<Agent> availableAgents = new HashSet<Agent>();

        public AgentServer(TcpListener listener)
        {
            this.listener = listener;
            
            
        }

        public void Start()
        {
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                ThreadNameSetter namesetter = new ThreadNameSetter();
                Thread thread = this.Factory.CreateThread(() => agent_Thread(client,namesetter));
                namesetter.Thread = thread;
                thread.Start();
            }
        }

        protected abstract AgentController CreateAgentController(AgentServer server, TcpClient client, Action<string> SetControllerName);

        private void agent_Thread(TcpClient client, ThreadNameSetter namesetter)
        {
            AgentController agent = CreateAgentController(this,client,namesetter.SetName);
            
            lock(this)
                this.agents.Add(agent);

            agent.Start();
        }

        public Agent TakeControlOf(string name)
        {
            lock (this)
            {
                Agent agent;
                if(this.knownAgents.TryGetValue(name,out agent))
                {
                    this.availableAgents.Remove(agent);
                    return agent;
                }
                else
                    throw new Exception("Agent controller was unable to assume control of "+name);                
            }
        }

        public void AddAgent(Agent agent)
        {
            lock (this)
            {
                this.knownAgents.Add(agent.Name, agent);
                this.availableAgents.Add(agent);
            }
        }

        private class ThreadNameSetter
        {
            private Thread thread;

            public Thread Thread
            {
                set { thread = value; }
            }

            internal void SetName(string name)
            {
                thread.Name = name;
            }
        }
    }
}
