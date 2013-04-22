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
        public SweetMap() : base(new Size(6, 6))
        {
			BuildMap ();
		}

		private void BuildMap()
		{
			/*	Map to be built:
			 * 
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
			 * 
			 * Legend:
			 * 	 'I' = Impassable Wall
			 *   'W' = Wall
			 *   'P' = Player
			 *   ' ' = Empty Tile
			 * 
			 */

			int start = -6;
			int stop = 5;
			int factor = 1;

			foreach (int idx in AlternateRange (-5, 5, 2)) {
				this.AddChunk<Wall> (new Point (idx, start * factor), new Point (idx, stop * factor));
				factor *= -1;
			}

			this [0, 0].AddEntity (new Player ());
		}

		public IEnumerable<int> AlternateRange(int start, int count, int inc) {
			for (int i = start; i < start + count; i += inc) {
				yield return i;
			}
		}

    }
}