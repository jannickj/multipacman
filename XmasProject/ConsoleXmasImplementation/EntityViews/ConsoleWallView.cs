namespace ConsoleXmasImplementation.EntityViews
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