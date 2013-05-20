using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsolePlayerView : ConsoleEntityView
	{

		public ConsolePlayerView(XmasEntity model, ThreadSafeEventManager evtman)
			: base(model, evtman)
		{
		}

		public override char Symbol
		{
			get { return 'P'; }
		}
	}
}