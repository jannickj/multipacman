using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Model.Events
{
    public class EisAgentDisconnectedEvent : XmasEvent
    {
        private Agent agent;

        public Agent Agent
        {
            get { return agent; }
        }

        public EisAgentDisconnectedEvent(Agent agent)
        {
            this.agent = agent;
        }
    }
}
