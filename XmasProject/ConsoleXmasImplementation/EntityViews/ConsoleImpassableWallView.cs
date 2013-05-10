using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.EntityViews
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