using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsolePlayerView : ConsoleEntityView
	{
		public ConsolePlayerView(XmasEntity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'P'; }
		}
	}
}