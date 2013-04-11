using System;
using GooseEngine.Percepts;
using GooseEngine.EIS.Percepts;
using GooseEngine.Data;

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
			throw new NotImplementedException();
		}

		#endregion



	}
}

