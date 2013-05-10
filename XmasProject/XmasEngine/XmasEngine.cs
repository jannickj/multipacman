using System.Collections.Generic;
using System.Threading;
using XmasEngine.Exceptions;
using XmasEngineController;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineView;

namespace XmasEngine
{
	public class XmasEngine
	{
		private XmasEngineFactory factory;

		private Thread modelThread;
		private List<Thread> otherThreads = new List<Thread>();

		public XmasEngine(XmasEngineFactory factory)
		{
			this.factory = factory;
		}

		public void StartEngine(XmasModel model, XmasView view, XmasController controller)
		{
			if (modelThread != null)
			{
				throw new EngineAlreadyStartedException();
			}
			XmasFactory fact = model.Factory;
			Thread modelt = fact.CreateThread(model.Start);
			Thread viewt = fact.CreateThread(view.Start);
			Thread cont = fact.CreateThread(controller.Start);

			modelt.Name = "Model Thread";
			viewt.Name = "View Thread";
			cont.Name = "Controller Thread";

			model.Initialize();
			view.Initialize();
			controller.Initialize();

			modelt.Start();
			viewt.Start();
			cont.Start();

			modelThread = modelt;
			otherThreads.Add(viewt);
			otherThreads.Add(cont);
		}
	}
}