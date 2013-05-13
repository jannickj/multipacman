using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View.EntityViews
{
	public class ConsoleAgentView : ConsoleEntityView
	{
		public ConsoleAgentView(XmasEntity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'A'; }
		}
	}
}