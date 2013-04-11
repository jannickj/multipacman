using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using GooseEngine.GameManagement;
using GooseEngine.Percepts;

namespace GooseEngine.EIS
{
    public class EISGameFactory : GameFactory
    {

        public override Vision CreateVision(Grid<Tile> grid, Entity owner)
        {
            return base.CreateVision(grid, owner);
        }
    }
}
