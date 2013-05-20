using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleGhostView : ConsoleEntityView
	{
		public ConsoleGhostView(XmasEntity model, ThreadSafeEventManager evtman)
			: base(model, evtman)
		{
		}

		public override char Symbol
		{
			get { return 'G'; }
		}
	}
}