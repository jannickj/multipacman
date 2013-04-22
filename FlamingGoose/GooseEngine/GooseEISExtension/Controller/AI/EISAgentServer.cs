using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GooseEISExtension.Model;
using GooseEngine.Entities.Units;
using GooseEngineController.AI;
using iilang.Parameters;

namespace GooseEISExtension.Controller.AI
{
	public class EISAgentServer : AgentServer
	{
		private TcpListener listener;
		private IILActionParser parser;
		private EISConversionTool tool;

		public EISAgentServer(TcpListener listener, EISConversionTool tool, IILActionParser parser) : base(listener)
		{
			this.listener = listener;
			this.tool = tool;
			this.parser = parser;
		}

		protected override void Initialize()
		{
			listener.Start();
		}

		protected override Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor()
		{
			TcpClient client = listener.AcceptTcpClient();


			return () =>
				{
					string name;
					AgentController value = CreateAgentController(client, out name);
					return new KeyValuePair<string, AgentController>(name, value);
				};
		}


		private AgentController CreateAgentController(TcpClient client, out string name)
		{
			XmlReader xreader = XmlReader.Create(new StreamReader(client.GetStream(), Encoding.UTF8));
			XmlWriterSettings wset = new XmlWriterSettings();
			wset.OmitXmlDeclaration = true;
			XmlWriter xwriter = XmlWriter.Create(client.GetStream());
			XmlSerializer serializer = new XmlSerializer(typeof (IILIdentifier));
			IILIdentifier ident = (IILIdentifier) serializer.Deserialize(xreader);
			name = ident.Value;
			Agent agent = TakeControlOf(name);


			EISAgentController con = new EISAgentController(agent, xreader, xwriter, tool, parser);

			return con;
		}
	}
}