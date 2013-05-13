using System;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineExtensions
{
	public class VisionRangeModule : EntityModule
	{
		private int visionRange;
		
		public int VisionRange {
			get { return visionRange; }
			set { visionRange = value; }
		}

		public VisionRangeModule (int visionRange)
		{
			this.visionRange = visionRange;
		}
	}
}

