using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GooseEngine;
using GooseEngine.Data.GenericEvents;
using GooseEngine.EIS;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using GooseEngineController.AI;
using iilang;

namespace GooseEngineController.EIS.AI
{
    public class EISAgentController : AgentController
    {

        private XmlReader xreader;
        private XmlWriter xwriter;
        XmlSerializer deserializer = new XmlSerializer(typeof(IILAction));
        XmlSerializer serializer = new XmlSerializer(typeof(IILPerceptCollection));
		EISConversionTool tool;
		IILActionParser actionparser;


        public EISAgentController(Agent agent, XmlReader xreader, XmlWriter xwriter, EISConversionTool tool, IILActionParser actionparser) 
			: base(agent)
        {
            this.xreader = xreader;
            this.xwriter = xwriter;
            this.PerceptsRecieved += EISAgentController_PerceptsRecieved;
			this.tool = tool;
			this.actionparser = actionparser;
        }

        private void update()
        {
			IILAction iilaction = (IILAction) deserializer.Deserialize (xreader);
			EISAction eisaction = actionparser.parseIILAction (iilaction);
			EntityGameAction gameaction = (EntityGameAction)tool.ConvertToGoose (eisaction);
			this.performAction (gameaction);
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

        void EISAgentController_PerceptsRecieved(object sender, UnaryValueEvent<PerceptCollection> evt)
        {
            IILPerceptCollection perceptcollection = (IILPerceptCollection)tool.ConvertToForeign(evt.Value);
			serializer.Serialize (xwriter, perceptcollection);
        }

        #endregion
    }
}
