using System;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class HealthModule : EntityModule
	{
		private int health;

		public int Health
		{
			get { return health; }
			set { health = value; }
		}

		public HealthModule (int health)
		{
			this.health = health;
		}
	}
}

