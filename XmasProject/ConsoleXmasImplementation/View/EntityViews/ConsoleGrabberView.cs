using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;


namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleGrabberView : ConsoleEntityView
	{
		public ConsoleGrabberView (XmasEntity model, ThreadSafeEventManager evtman)
			: base(model, evtman)
		{
		}

		public override char Symbol {
			get { return 'G'; }
		}
	}
}

