using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JSLibrary.IiLang.Parameters;
using XmasEngineController.AI;
using XmasEngineExtensions.EisExtension.Model;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace XmasEngineExtensions.EisExtension.Controller.AI
{
	public class EISAgentServer : AgentManager
	{
		private TcpListener listener;
		private IILActionParser parser;
		private EisConversionTool tool;

		public EISAgentServer(TcpListener listener, EisConversionTool tool, IILActionParser parser)
		{
			this.listener = listener;
			this.tool = tool;
			this.parser = parser;
            
		}

		public override void Initialize()
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

			StreamReader sreader = new StreamReader(client.GetStream(), Encoding.UTF8);
            XmlReaderSettings rset = new XmlReaderSettings();
            rset.ConformanceLevel = ConformanceLevel.Fragment;
			XmlReader xreader = XmlReader.Create(sreader,rset);

			XmlWriterSettings wset = new XmlWriterSettings();
			wset.OmitXmlDeclaration = true;
			wset.ConformanceLevel = ConformanceLevel.Fragment;
			XmlWriter xwriter = XmlWriter.Create(client.GetStream());
			XmlSerializer serializer = new XmlSerializer(typeof (IilIdentifier));
			IilIdentifier ident = (IilIdentifier) serializer.Deserialize(xreader);
			name = ident.Value;
			Agent agent = TakeControlOf(name);


			EISAgentController con = new EISAgentController(agent, xreader, xwriter, tool, parser);

			return con;
		}

       
	}
}