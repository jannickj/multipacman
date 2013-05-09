﻿using System.Xml;
using System.Xml.Serialization;
using GooseEISExtension.Model;
using GooseEngine;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using GooseEngineController.AI;
using JSLibrary.Data.GenericEvents;
using iilang;
using iilang.DataContainers;

namespace GooseEISExtension.Controller.AI
{
	public class EISAgentController : AgentController
	{
		private IILActionParser actionparser;
		private XmlSerializer deserializer = new XmlSerializer(typeof (IILAction));
		private XmlSerializer serializer = new XmlSerializer(typeof (IILPerceptCollection));
		private EISConversionTool tool;
		private XmlReader xreader;
		private XmlWriter xwriter;


		public EISAgentController(Agent agent, XmlReader xreader, XmlWriter xwriter, EISConversionTool tool,
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
			IILAction iilaction = (IILAction) deserializer.Deserialize(xreader);
			EISAction eisaction = actionparser.parseIILAction(iilaction);
			EntityGameAction gameaction = (EntityGameAction) tool.ConvertToGoose(eisaction);
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
			IILPerceptCollection perceptcollection = (IILPerceptCollection) tool.ConvertToForeign(evt.Value);
			serializer.Serialize(xwriter, perceptcollection);
		}

		#endregion
	}
}