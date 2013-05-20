using System;
using XmasEngineView;
using XmasEngineModel.Management;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.LoggerExtension;

namespace ConsoleXmasImplementation.ConsoleLogger
{
	public class LoggerViewFactory : ViewFactory
	{
		private ThreadSafeEventManager evtman;
		private Logger logstream;

		public LoggerViewFactory (ThreadSafeEventManager evtman, Logger logstream)
		{
			this.evtman = evtman;
			this.logstream = logstream;
		}

		#region implemented abstract members of ViewFactory

		public override EntityView ConstructEntityView (XmasEntity model)
		{
			return new LoggerEntityView(model, evtman, logstream);
		}

		#endregion
	}
}

