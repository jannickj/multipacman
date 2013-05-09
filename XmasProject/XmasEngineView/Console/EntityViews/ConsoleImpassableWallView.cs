using XmasEngineModel;

namespace XmasEngineView.Console.EntityViews
{
	public class ConsoleImpassableWallView : ConsoleEntityView
	{
		public ConsoleImpassableWallView(Entity model)
			: base(model)
		{
		}
	
		public override char Symbol
		{
			get { return 'I'; }
		}
	}
}

