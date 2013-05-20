using System.Xml;
using System.Xml.Serialization;
using JSLibrary.Data.GenericEvents;
using JSLibrary.IiLang;
using JSLibrary.IiLang.DataContainers;
using XmasEngineController.AI;
using XmasEngineExtensions.EisExtension.Model;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Controller.AI
{
	public class EISAgentController : AgentController
	{
		private IILActionParser actionparser;
		private XmlSerializer deserializer = new XmlSerializer(typeof (IilAction));
		private XmlSerializer serializer = new XmlSerializer(typeof (IilPerceptCollection));
		private EisConversionTool tool;
		private XmlReader xreader;
		private XmlWriter xwriter;


		public EISAgentController(Agent agent, XmlReader xreader, XmlWriter xwriter, EisConversionTool tool,
		                          IILActionParser actionparser)
			: base(agent)
		{
			this.xreader = xreader;
			this.xwriter = xwriter;
			PerceptsRecieved += EISAgentController_PerceptsRecieved;
			this.tool = tool;
			this.actionparser = actionparser;
		}

		private void update()
		{
			IilAction iilaction = null;

			while (iilaction == null)
			{
				iilaction = (IilAction)deserializer.Deserialize(xreader);
				
				if (iilaction == null)
					xreader.ReadEndElement();
			}
			
			
			EISAction eisaction = actionparser.parseIILAction(iilaction);
			EntityXmasAction gameaction = (EntityXmasAction) tool.ConvertToXmas(eisaction);
			performAction(gameaction);
		}

		#region implemented abstract members of AgentController

		public override void Start()
		{
			while (true)
			{
				update();
			}
		}

		#endregion

		#region EVENTS

		private void EISAgentController_PerceptsRecieved(object sender, UnaryValueEvent<PerceptCollection> evt)
		{
			IilPerceptCollection perceptcollection = (IilPerceptCollection) tool.ConvertToForeign(evt.Value);
			serializer.Serialize(xwriter, perceptcollection);
		}

		#endregion
	}
}