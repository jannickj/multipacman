using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleWallView : ConsoleEntityView
	{
		public ConsoleWallView(XmasEntity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'W'; }
		}
	}
}