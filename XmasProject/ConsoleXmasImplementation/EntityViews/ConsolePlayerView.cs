using XmasEngineModel;

namespace XmasEngineView.Console.EntityViews
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

