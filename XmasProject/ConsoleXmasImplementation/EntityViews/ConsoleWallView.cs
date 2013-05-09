using XmasEngineModel;

namespace ConsoleXmasImplementation.EntityViews
{
	public class ConsoleWallView : ConsoleEntityView
	{
		public ConsoleWallView(Entity model)
			: base(model)
		{
		}

		public override char Symbol
		{
			get { return 'W'; }
		}
	}
}