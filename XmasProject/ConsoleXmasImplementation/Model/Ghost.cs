using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model
{
	public class Ghost : Agent
	{
		public Ghost(string name) : base(name)
		{
			this.RegisterModule(new SpeedModule(1000));
		}
	}
}
