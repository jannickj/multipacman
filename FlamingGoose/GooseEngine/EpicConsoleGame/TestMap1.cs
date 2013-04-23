using System.Collections.Generic;
using GooseEngine;
using GooseEngine.Entities;
using GooseEngine.Entities.Units;
using JSLibrary.Data;

namespace EpicConsoleGame
{
	public class TestMap1 : GooseMap
	{
		public TestMap1() : base(new Size(6, 6))
		{
			BuildMap();
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

			foreach (int idx in AlternateRange(-5, 5, 2))
			{
				AddChunk<Wall>(new Point(idx, start*factor), new Point(idx, stop*factor));
				factor *= -1;
			}

			this[0, 0].AddEntity(new Player());
		}

		public IEnumerable<int> AlternateRange(int start, int count, int inc)
		{
			for (int i = start; i < start + count; i += inc)
			{
				yield return i;
			}
		}
	}
}