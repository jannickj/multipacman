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

		public iilang.IILPercept toIILang ()
		{
			IILPercept percept = new IILPercept ("vision");

			foreach (KeyValuePair<Point, Tile> kvp in VisibleTiles)
			{
				IILFunction fun = new IILFunction ("on",
				                                   new IILNumeral(kvp.Key.X),
				                                   new IILNumeral(kvp.Key.Y),
				                                   new IILIdentifier(kvp.Value.ToString())
				                                   );
				percept.addParameter(fun);
			}

			return percept;
	 	}


		#endregion



	}
}

