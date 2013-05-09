using XmasEngineModel;

namespace ConsoleXmasImplementation.EntityViews
{
	public class ConsolePlayerView : ConsoleEntityView
	{
		public ConsolePlayerView(Entity model)
			: base(model)
		{
		}
		
		public override char Symbol
		{
			get { return 'P'; }
		}
	}
}

