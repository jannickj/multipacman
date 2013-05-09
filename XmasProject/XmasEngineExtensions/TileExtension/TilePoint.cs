using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Data;
using XmasEngineModel.World;

namespace XmasEngineExtensions.TileExtension
{
	

	public class TilePosition : XmasPosition
	{
		private Point point;

		public TilePosition(Point p)
		{
			point = p;
		}

		public Point Point
		{
			get { return point; }
		}
	}
}
