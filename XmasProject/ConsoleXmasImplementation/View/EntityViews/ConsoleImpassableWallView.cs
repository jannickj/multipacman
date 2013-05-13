using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleImpassableWallView : ConsoleEntityView
	{
		public ConsoleImpassableWallView(XmasEntity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'I'; }
		}
	}
}