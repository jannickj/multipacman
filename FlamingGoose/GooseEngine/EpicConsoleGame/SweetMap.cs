using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;

namespace EpicConsoleGame
{
    public class SweetMap : GooseMap
    {
		private List<KeyValuePair<Point,Point>> walls;
        public SweetMap() : base(new Size(6, 6))
        {
			BuildMap ();
		}

		private void BuildMap()
		{
			/*
			 * IIIIIIIIIIIIIII
			 * I W   W   W   I 
			 * I W W W W W W I
			 * I W W W W W W I
			 * I W W W W W W I 
			 * I W W W W W W I 
			 * I W W W W W W I 
			 * I W W WPW W W I 
			 * I W W W W W W I 
			 * I W W W W W W I 
			 * I W W W W W W I 
			 * I W W W W W W I 
			 * I W W W W W W I 
			 * I   W   W   W I 
			 * IIIIIIIIIIIIIII
			 */

			int start = -6;
			int stop = 5;
			int factor = 1;

			foreach (int index in AlternateRange (-5, 5, 2)) {
				this.AddChunk<Wall> (new Point (i, start * factor), new Point (i, stop * factor));
				factor *= -1;
			}
		}

		public IEnumerable<int> AlternateRange(int start, int count, int inc) {
			for (int i = start; i < start + count; i += inc) {
				yield return i;
			}
		}

    }
}
