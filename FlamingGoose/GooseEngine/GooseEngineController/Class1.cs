using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Entities.Units;

namespace GooseEngineController
{
    public class AgentController
    {
        Agent model;
        Socket goalsocket;

        public AgentController(Agent model, Socket goalsocket)
        {
            this.model = model;
            this.goalsocket = goalsocket;

        }



    }
}
