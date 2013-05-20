using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleWallView : ConsoleEntityView
	{
		public ConsoleWallView(XmasEntity model, ThreadSafeEventManager evtman)
			: base(model, evtman)
		{
		}

		public override char Symbol
		{
			get { return 'W'; }
		}
	}
}