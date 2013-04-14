using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GooseEngine.Data.GenericEvents;
using GooseEngine.Entities.Units;
using iilang;

namespace GooseEngine.AI.BuiltInController
{
    public class EISAgentController : AgentController
    {

        private XmlReader xreader;
        private XmlWriter xwriter;
        XmlSerializer deserializer = new XmlSerializer(typeof(IILAction));
        XmlSerializer serializer = new XmlSerializer(typeof(IILAction));

        public EISAgentController(Agent agent, XmlReader xreader, XmlWriter xwriter) : base(agent)
        {
            this.xreader = xreader;
            this.xwriter = xwriter;
            this.PerceptsRecieved += EISAgentController_PerceptsRecieved;

        }

        private void update()
        {
            
            

        }


		#region implemented abstract members of AgentController

		public override void Start ()
		{
            while (true)
            {
                update();
            }

		}

		#endregion

        #region EVENTS

        void EISAgentController_PerceptsRecieved(object sender, ValueEvent<ICollection<IPercept>> value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
