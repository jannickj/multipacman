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
			
		}

		protected override SpeedModule ConstructSpeedModule()
		{
			return new SpeedModule(100);
		}
	}
}
