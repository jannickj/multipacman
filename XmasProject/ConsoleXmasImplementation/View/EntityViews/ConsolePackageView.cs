using System;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsolePackageView : ConsoleEntityView
	{
		public ConsolePackageView (XmasEntity model, ThreadSafeEventManager evtman)
			:base(model, evtman)
		{
		}

		#region implemented abstract members of ConsoleEntityView

		public override char Symbol {
			get { return 'X'; }
		}

		#endregion
	}
}

