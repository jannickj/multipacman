using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;

namespace GooseEngine.Percepts
{
    public class Vision : Percept
    {
        private List<KeyValuePair<Point, Tile>> visibleTiles = new List<KeyValuePair<Point, Tile>>();
        private Grid<Tile> grid;

        public Vision(Grid<Tile> grid)
        {
            this.grid = grid;
        }

        public ICollection<KeyValuePair<Point, Tile>> VisibleTiles {
			get { return visibleTiles; }
			private set;
        }

		private bool isTileVisible(Point tile)
		{
			List<Point> cornerize = new List<Point> () { 
				new Point(0,0), 
				new Point(1,0), 
				new Point(0,1), 
				new Point(1,1) };

			IEnumerable<Point> centerCorners = cornerize.Select (corner => corner + grid.Center); // should be done only once for efficiency
			IEnumerable<Point> tileCorners = cornerize.Select (corner => corner + grid.Center);

			// check if ANY corner of the center tile connects to ALL corners of the destination tile
			return centerCorners.Any (cc => tileCorners.All (tc => connectCorner (cc, tc)));
		}

		private bool connectCorner(Point origin, Point destination)
		{
			for (int i = Math.Min(origin.X, destination.X); i < Math.Max(origin.X, destination.X); i++) {

			}
		}

    }
}
