using ConsoleXmasImplementation.Model.Modules;
using XmasEngineExtensions.TileExtension.Modules;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class GrabberAgent : ConsoleAgent
	{
		public GrabberAgent (string name) : base(name)
		{
			RegisterModule(new PackageGrabbingModule(false));
		}

		protected override SpeedModule ConstructSpeedModule ()
		{
			return new SpeedModule (200);
		}
	}
}

