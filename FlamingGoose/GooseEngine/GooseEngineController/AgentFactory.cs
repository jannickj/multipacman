using GooseEngineController.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngineController
{
    public abstract class AgentFactory
    {
        public abstract AgentServer ContructServer();

    }
}
