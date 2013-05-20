using System.Collections.Generic;
using System.Threading;
using XmasEngine.Exceptions;
using XmasEngineController;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
	public class XmasEngineManager
	{
		private XmasEngineFactory factory;

		private Thread modelThread;
		private List<Thread> viewThreads = new List<Thread>();
		private List<Thread> controllerThreads = new List<Thread>();

		public XmasEngineManager(XmasEngineFactory factory)
		{
			this.factory = factory;
		}

		public void StartEngine(XmasModel model,ICollection<XmasView> views, ICollection<XmasController> controllers)
		{
			if (modelThread != null)
			{
				throw new EngineAlreadyStartedException();
			}
			XmasFactory fact = model.Factory;
			Thread modelt = fact.CreateThread(model.Start);

			model.Initialize();

			int i = 1;

			foreach (var xmasView in views)
			{
				xmasView.Initialize();
				Thread viewt = fact.CreateThread(xmasView.Start);
				viewt.Name = "View Thread "+i;
				i++;
				viewThreads.Add(viewt);
			}

			i = 1;
			foreach (var xmasController in controllers)
			{
				xmasController.Initialize();
				Thread cont = fact.CreateThread(xmasController.Start);
				cont.Name = "Controller Thread " + i;
				i++;
				controllerThreads.Add(cont);
			}

			modelt.Name = "Model Thread";
			modelThread = modelt;

			modelt.Start();
			foreach (var viewThread in viewThreads)
			{
				viewThread.Start();
			}

			foreach (var controllerThread in controllerThreads)
			{
				controllerThread.Start();
			}

			
			
		}
	}
}