using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model
{
	public class Ghost : ConsoleAgent
	{
		public Ghost(string name) : base(name)
		{
			this.RegisterModule(new VisionModule());
			this.RegisterModule(new VisionRangeModule(5));
		}

		protected override SpeedModule ConstructSpeedModule()
		{
			return new SpeedModule(100);
		}
	}
}
