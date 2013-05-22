using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.EisExtension.Model.Events;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Model.XmasActions
{
    public class EisAgentDisconnected : EnvironmentAction
    {
        private Agent agent;

        public EisAgentDisconnected(Agent agent)
        {
            this.agent = agent;
        }

        protected override void Execute()
        {
            this.EventManager.Raise(new EisAgentDisconnectedEvent(agent));
        }
    }
}
