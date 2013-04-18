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
        private Dictionary<AgentController, AgentControllerInfomation> agents = new Dictionary<AgentController, AgentControllerInfomation>();
        private Dictionary<string, Agent> knownAgents = new Dictionary<string, Agent>();
        private HashSet<Agent> availableAgents = new HashSet<Agent>();


        public AgentServer(TcpListener listener)
        {
            this.listener = listener;
            
            
        }

        protected abstract void Initialize();


        public void Start()
        {
            Initialize();
            while (true)
            {

                Func<KeyValuePair<string, AgentController>> agentcontroller = AquireAgentControllerContructor();
                Thread thread = null;
                thread = this.Factory.CreateThread(() => agent_Thread(agentcontroller));
                thread.Start();

            }
        }

        protected abstract Func<KeyValuePair<string,AgentController>> AquireAgentControllerContructor();

 
        private void agent_Thread(Func<KeyValuePair<string, AgentController>> constructor)
        {

            KeyValuePair<string, AgentController> agent;
            try
            {
                TryExecute<KeyValuePair<string, AgentController>>(constructor, 2000, out agent);
                AgentControllerInfomation ainfo = new AgentControllerInfomation();
                ainfo.Thread = Thread.CurrentThread;
                ainfo.Name = agent.Key;
                lock (this)
                    this.agents.Add(agent.Value, ainfo);

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

        private class AgentControllerInfomation
        {
            private Thread thread;
            private Agent controlling;
            private String name;

            private string ThreadName()
            {                  
                return name+ " Controller Thread";
            }

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
                    if(name != null && thread.Name == null)
                        thread.Name = ThreadName();
                }
            }

            public AgentControllerInfomation()
            {
               
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
            var t = default(T);
            var thread = new Thread(() => t = func());
            thread.Start();
            var completed = thread.Join(timeout);
            if (!completed)
            {
                thread.Abort();
                throw new TimeoutException();
            }
            result = t;
            return completed;
        }
    }
}
