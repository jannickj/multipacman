using System;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class SpeedModule : EntityModule
	{
		private double speed;

		public double Speed {
			get { return speed; }
			set { speed = value; }
		}

		public SpeedModule (double speed)
		{
			this.speed = speed;
		}
	}
}

