using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleDropZoneView : ConsoleEntityView
	{
		public ConsoleDropZoneView(XmasEntity model, ThreadSafeEventManager evtman)
			: base(model, evtman)
		{
		}
		
		public override char Symbol
		{
			get { return 'D'; }
		}
	}
}