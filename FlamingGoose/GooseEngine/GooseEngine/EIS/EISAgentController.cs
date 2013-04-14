using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GooseEngine.Data.GenericEvents;
using GooseEngine.Entities.Units;
using iilang;
using GooseEngine.EIS;
using GooseEngine.GameManagement;
using GooseEngine.EIS.Percepts;

namespace GooseEngine.AI.BuiltInController
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

        void EISAgentController_PerceptsRecieved(object sender, UnaryValueEvent<ICollection<Percept>> evt)
        {
			IILPerceptCollection perceptcollection = tool.ConvertToForeign (evt.Value);
			serializer.Serialize (xwriter, perceptcollection);
        }

        #endregion
    }
}
