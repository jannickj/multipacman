﻿using System.Threading;
using GooseEISExtension;
using GooseEngine;
using GooseEngineController;
using GooseEngineView;
using GooseEngineManager;
using System.Net;
using System;

namespace EpicConsoleGame
{
	internal class Program
	{
		private static void Main(string[] args)
		{
            ConsoleGooseImplFactory factory = new ConsoleGooseImplFactory();

			var t = factory.FullConstruct(new TestMap1(),new EisAgentFactory(IPAddress.Parse("127.0.0.1"),133766));

            factory.StartEngine(t.Item1, t.Item2, t.Item3);
		}
	}
}