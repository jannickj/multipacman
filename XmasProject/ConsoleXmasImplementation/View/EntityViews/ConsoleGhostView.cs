using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleGhostView : ConsoleEntityView
	{
		public ConsoleGhostView(XmasEntity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'G'; }
		}
	}
}