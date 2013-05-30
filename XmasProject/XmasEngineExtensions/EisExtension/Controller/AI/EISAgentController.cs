using System.Xml;
using System.Xml.Serialization;
using JSLibrary.Data.GenericEvents;
using JSLibrary.IiLang;
using JSLibrary.IiLang.DataContainers;
using XmasEngineController.AI;
using XmasEngineExtensions.EisExtension.Model;
using XmasEngineExtensions.EisExtension.Model.Events;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using JSLibrary.Network;
using System.IO;
using JSLibrary;
using System;
using System.Threading;
using System.Net.Sockets;
using XmasEngineModel.Management.Actions;
using System.Diagnostics;

namespace XmasEngineExtensions.EisExtension.Controller.AI
{
	public class EISAgentController : AgentController
	{
		#region DEBUG
		private DateTime dt = DateTime.Now;
		#endregion

		private IILActionParser actionparser;
		private XmlSerializer deserializer = new XmlSerializer(typeof (IilAction));
		private XmlSerializer serializer = new XmlSerializer(typeof (IilPerceptCollection));
		private EisConversionTool tool;
		private StreamReader sreader;
        private StreamWriter swriter;
        private PacketStream packetstream;
        private ActionManager actman;
        private TcpClient client;

		public EISAgentController(Agent agent, TcpClient client, ActionManager actman, PacketStream packetstream, StreamReader sreader, StreamWriter swriter, EisConversionTool tool,
		                          IILActionParser actionparser)
			: base(agent)
		{
            this.client = client;
            this.packetstream = packetstream;
            this.sreader = sreader;
            this.swriter = swriter;
			PerceptsRecieved += EISAgentController_PerceptsRecieved;
			this.tool = tool;
			this.actionparser = actionparser;
            this.actman = actman;
		}

		private void update()
		{
			Stopwatch sw = new Stopwatch();

            packetstream.ReadNextPackage();

			IilAction iilaction = (IilAction)deserializer.Deserialize(sreader);

			sw.Start();
			EISAction eisaction = actionparser.parseIILAction(iilaction);
			EntityXmasAction gameaction = (EntityXmasAction) tool.ConvertToXmas(eisaction);
			sw.Stop();

			string description = String.Format("Received socket package with action: {0}", gameaction);
			actman.Queue(new SimpleAction(sa => sa.EventManager.Raise(new EisAgentTimingEvent(Agent, description, sw.Elapsed))));
			performAction(gameaction);
		}

		#region implemented abstract members of AgentController

		public override void Start()
		{
            try
            {
                while (true)
                {
                    update();
                }
            }
            catch(Exception e)
            {
                this.actman.Queue(new SimpleAction(sa => sa.EventManager.Raise(new EisAgentDisconnectedEvent(this.Agent,e))));
            }
		}

		#endregion

		#region EVENTS

		private void EISAgentController_PerceptsRecieved(object sender, UnaryValueEvent<PerceptCollection> evt)
		{
			IilPerceptCollection perceptcollection = (IilPerceptCollection) tool.ConvertToForeign(evt.Value);
			serializer.Serialize(swriter, perceptcollection);
			swriter.Flush();
			DateTime now = DateTime.Now;
			long diff = now.Ticks - dt.Ticks;
			dt = now;
		}

		#endregion
	}
}