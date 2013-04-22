using System.Collections.Generic;
using GooseEngine;
using GooseEngine.Entities;
using GooseEngine.Entities.Units;
using JSLibrary.Data;

namespace EpicConsoleGame
{
	public class SweetMap : GooseMap
	{
		private List<KeyValuePair<Point, Point>> walls;

		public SweetMap() : base(new Size(6, 6))
		{
			BuildMap();
			walls = new List<KeyValuePair<Point, Point>>
				{
					new KeyValuePair<Point, Point>(new Point(-4, -6), new Point(-4, 5)),
					new KeyValuePair<Point, Point>(new Point(-2, -4), new Point(-2, 6))
				};
		}

		private void BuildMap()
		{
			foreach (KeyValuePair<Point, Point> kv in walls)
				AddChunk<Wall>(kv.Key, kv.Value);
			this[0, 0].AddEntity(new Player());
		}
	}
}