using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleImpassableWallView : ConsoleEntityView
	{
		public ConsoleImpassableWallView(XmasEntity model, ThreadSafeEventManager evtman)
			: base(model, evtman)
		{
		}

		public override char Symbol
		{
			get { return 'I'; }
		}
	}
}