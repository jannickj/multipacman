using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using GooseEngine.AI;
using GooseEngine.AI.BuiltInController;
using GooseEngine.Entities.Units;
using iilang;

namespace GooseEngine.EIS
{
    public class EISAgentServer : AgentServer
    {
        private EISConversionTool tool;
        private IILActionParser parser;

        public EISAgentServer(TcpListener listener, EISConversionTool tool, IILActionParser parser) : base(listener)
        {
            this.tool = tool;
            this.parser = parser;
        }

        protected override AgentController CreateAgentController(AgentServer server, TcpClient client)
        {
            XmlReader xreader = XmlReader.Create(new StreamReader(client.GetStream(),Encoding.UTF8));
            XmlWriterSettings wset = new XmlWriterSettings();
            wset.OmitXmlDeclaration = true;
            XmlWriter xwriter = XmlWriter.Create(client.GetStream());
            XmlSerializer serializer = new XmlSerializer(typeof(IILIdentifier));
            IILIdentifier ident = (IILIdentifier) serializer.Deserialize(xreader);
            Agent agent = server.Find(ident.Value);
            
            EISAgentController con = new EISAgentController(agent,xreader,xwriter,tool,parser);

            return con;
        }
    }
}
