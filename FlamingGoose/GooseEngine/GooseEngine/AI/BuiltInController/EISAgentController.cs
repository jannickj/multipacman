using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GooseEngine.Entities.Units;

namespace GooseEngine.AI.BuiltInController
{
    public class EISAgentController : AgentController
    {

        public EISAgentController(Agent agent, XmlTextReader xreader) : base(agent)
        {

        }

		#region implemented abstract members of AgentController

		public override void Start ()
		{
			throw new NotImplementedException ();
		}

		#endregion
    }
}
