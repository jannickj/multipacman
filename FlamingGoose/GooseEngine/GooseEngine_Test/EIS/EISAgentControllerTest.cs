﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using GooseEngine.AI.BuiltInController;
using GooseEngine.Entities.Units;
using NUnit.Framework;
using GooseEngine.EIS;
using GooseEngine;
using GooseEngine.EIS.Conversion.IILang.Actions;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using GooseEngine.GameManagement;
using GooseEngine.EIS.Conversion.IILang;

namespace GooseEngine_Test.EIS
{

    [TestFixture]
    public class EISAgentControllerTest
    {

        bool lock1 = true;
        EISAgentController controller;
        [Test]
        public void SingleUpdate_RecievedGetAllPercepts_PickUpPerceptsAndReturnThemThroughWriter()
        {

            ActionManager manager = new ActionManager();
            GameWorld world = new GameWorld(new GameMap(new GooseEngine.Data.Size(4, 4)));
            GameFactory fact = new GameFactory(manager);
            Agent agent = new Agent();
            agent.ActionManager = manager;
            agent.World = world;
            agent.Factory = fact;
            world.AddEntity(new GooseEngine.Data.Point(0, 0), agent);


            XElement node = XElement.Parse(
                @"<action name=""getAllPercepts"">
				</action>");

            Thread thread1 = new Thread(new ThreadStart(test2));
            thread1.Name = "TCP Server";
            thread1.Start();

            while (lock1) { }
            TcpClient client = new TcpClient("localhost", 6661);
            Stream stream = client.GetStream();
    
            StringBuilder sb = new StringBuilder();
            

            EISConversionTool ctool = new EISConversionTool();
            ctool.AddConverter(new GetAllPerceptsActionConverter());
            ctool.AddConverter(new EISPerceptCollectionSerializer());
            ctool.AddConverter(new EISVisionSerializer());
            ctool.AddConverter(new EISSingleNumeralSerializer());

            IILActionParser parser =new IILActionParser();

            XmlReaderSettings set = new XmlReaderSettings();
            set.ConformanceLevel = ConformanceLevel.Fragment;
            XmlReader xreader = new XmlTextReader(new StreamReader(stream, Encoding.UTF8));
            XmlWriter xwriter = XmlWriter.Create(sb);
            controller = new EISAgentController(agent, xreader, xwriter, ctool, parser);


            Thread thread2 = new Thread(new ThreadStart(test1));
            thread2.Name = "Controller";
            thread2.Start();

            while(manager.ExecuteActions() == 0);
            string returned = sb.ToString();


        }

        private void test2()
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse("127.0.0.1"), 6661);
            tcp.Start();
            lock1 = false;
            TcpClient client = tcp.AcceptTcpClient();
            string actiontext = @"<?xml version=""1.0"" encoding=""utf-16""?><action name=""getAllPercepts"">
				</action>";

            StreamWriter writer = new StreamWriter(client.GetStream(),Encoding.UTF8);
            writer.Write(actiontext);
            writer.Flush();


        }

        private void test1()
        {
            controller.Start();
        }


    }
}
