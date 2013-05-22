using System;
using XmasEngineExtensions.TileExtension.Modules;

namespace ConsoleXmasImplementation.Model
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

