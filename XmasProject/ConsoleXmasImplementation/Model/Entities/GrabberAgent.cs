using XmasEngineExtensions.TileExtension.Modules;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class GrabberAgent : ConsoleAgent
	{
		public GrabberAgent (string name) : base(name)
		{
		}

		protected override SpeedModule ConstructSpeedModule ()
		{
			return new SpeedModule (200);
		}
	}
}

