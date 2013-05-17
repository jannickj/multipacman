using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Entities;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation
{
	public class TestWorld1 : TileWorldBuilder
	{
		public TestWorld1() : base(new Size(6, 6))
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

			//foreach (int idx in AlternateRange(-5, 5, 2))
			//{
			//	AddChunk(() => new Wall(), new Point(idx, start*factor), new Point(idx, stop*factor));
			//	factor *= -1;
			//}

			this.AddEntity(new Wall(), new Point(0, 1));
			this.AddEntity(new Wall(), new Point(1, 1));
			this.AddEntity(new Player(), new Point(0, 0));
            this.AddEntity(new Agent("testname"), new Point(1, 0));
			
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