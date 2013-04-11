using System;
using GooseEngine.Percepts;
using GooseEngine.EIS.Percepts;
using GooseEngine.Data;
using System.Collections.Generic;
using iilang;

namespace GooseEngine.EIS.EISPercepts
{
	public class EISVision : Vision, IEISPercept
	{
		public EISVision (Grid<Tile> grid, Entity owner) : base(grid, owner)
		{
		}

		#region IEISPercept implementation1

		public iilang.EisPercept toIILang ()
		{
			EisPercept percept = new EisPercept ("vision");

			foreach (KeyValuePair<Point, Tile> kvp in VisibleTiles)
			{
				EisFunction fun = new EisFunction ("on",
				                                   new EisNumeral(kvp.Key.X),
				                                   new EisNumeral(kvp.Key.Y),
				                                   new EisIdentifier(kvp.Value.ToString())
				                                   );
				percept.addParameter(fun);
			}

			return percept;
	 	}


		#endregion



	}
}

