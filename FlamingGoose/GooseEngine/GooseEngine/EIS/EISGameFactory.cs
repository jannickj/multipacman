using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using GooseEngine.GameManagement;
using GooseEngine.Percepts;
using GooseEngine.EIS.Percepts;

namespace GooseEngine.EIS
{
    public class EISGameFactory : GameFactory
    {
		public EISGameFactory(ActionManager actman) : base (actman)
		{
		}

        public override Vision CreateVisionPercept(Grid<Tile> grid, Entity owner)
        {
            return base.CreateVisionPercept(grid, owner);
        }

		public override SingleNumeralPercept CreateSingleNumeralPercept (string name, double value)
		{
			return new EISSingleNumeralPercept (name, value);
		}
    }
}
