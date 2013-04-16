using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GooseEngine.Entities.Units;

namespace GooseEngine.AI
{
    public abstract class AgentServer : GooseActor
    {
        private TcpListener listener;
        private List<AgentController> agents = new List<AgentController>();
        private Dictionary<string, Agent> knownAgents = new Dictionary<string, Agent>();

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
                Thread thread = this.Factory.CreateThread(() => agent_Thread(client));
                thread.Start();
            }
        }

        protected abstract AgentController CreateAgentController(AgentServer server, TcpClient client);

        private void agent_Thread(TcpClient client)
        {
            AgentController agent = CreateAgentController(this,client);
            
            lock(this)
                this.agents.Add(agent);

            agent.Start();
        }

        public Agent Find(string name)
        {
            lock (this)
            {
                return this.knownAgents["name"];
            }
        }

        public void AddAgent(Agent agent)
        {
            lock (this)
            {
                this.knownAgents.Add(agent.Name, agent);
            }
        }
    }
}
