using System;
using System.Collections.Generic;
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
		private Entity owner;

        public Vision(Grid<Tile> grid, Entity owner)
        {
            this.grid = grid;
			this.owner = owner;
			FindVisibleTiles ();
        }

		public Entity Owner {
			get { return owner;}
		}

        public ICollection<KeyValuePair<Point, Tile>> VisibleTiles {
			get { return visibleTiles; }
        }

		private void FindVisibleTiles2()
		{
			HashSet<Point> explored = new HashSet<Point> ();
			HashSet<Point> frontier = new HashSet<Point> ();
//			Point current = grid.Center;
			frontier.Add (grid.Center);

			foreach (Point f in frontier) {
				frontier.Remove(f);
				explored.Add(f);
			
				foreach (Point p in grid.getAdjacent(f)) {
					if (isTileVisible(p)) 
					{
						visibleTiles.Add(new KeyValuePair<Point, Tile>(p, grid[p.X,p.Y]));
						
						if (!grid[p.X, p.Y].IsVisionBlocking (owner) && !explored.Contains(p))
							frontier.Add(p);
					}
				}
			}
		}

		private void FindVisibleTiles()
		{
			for (int x = 0; x < grid.Size.Width; x++)
				for (int y = 0; y < grid.Size.Height; y++)
					if (isTileVisible (new Point (x, y)))
						visibleTiles.Add (new KeyValuePair<Point, Tile> (new Point (x, y) - grid.Center, grid [x, y]));
		}

		public List<KeyValuePair<Point,Tile>> AllTiles()
		{
			List<KeyValuePair<Point,Tile>> tiles = new List<KeyValuePair<Point, Tile>> ();
			for (int x = 0; x < grid.Size.Width; x++)
				for (int y = 0; y < grid.Size.Height; y++)
					tiles.Add (new KeyValuePair<Point, Tile> (new Point (x, y) - grid.Center, grid [x, y]));
			return tiles;
		}

		private bool isTileVisible(Point tile)
		{
			List<Point> cornerize = new List<Point> () { 
				new Point(0,0), 
				new Point(1,0), 
				new Point(0,1), 
				new Point(1,1) };

			IEnumerable<Point> centerCorners = cornerize.Select (corner => corner + grid.Center); // should be done only once for efficiency
			IEnumerable<Point> tileCorners = cornerize.Select (corner => corner + tile);

			if (grid [tile.X, tile.Y].IsVisionBlocking (owner)) {
				// if the destination tile is vision blocking, we check if ANY corner of the center tile connects to any two corners of the destination tile
				return centerCorners.Any (cc => tileCorners.Count (tc => connectCorner (cc, tc)) >= 2);
			} else {
				// otherwise, check if ANY corner of the center tile connects to ALL corners of the destination tile
				return centerCorners.Any (cc => tileCorners.All (tc => connectCorner (cc, tc)));
			}
		}

		private bool connectCorner(Point origin, Point destination)
		{
			Vector v = new Vector (origin, destination);
			foreach (Point p in walkAlongVector(v)) {
				Point transp = origin + p;
				bool blocking = grid[transp.X, transp.Y].IsVisionBlocking(owner);
				if ( grid[transp.X, transp.Y].IsVisionBlocking(owner))
					return false;
			}

			return true;
		}

		private IEnumerable<Point> walkAlongVector(Vector v)
		{
			double linepiece = Math.Abs ((double)v.Y / (double)v.X);
			for (int i = 0; i < Math.Abs(v.X); i++) {
				int start = (int)Math.Floor (i * linepiece);
				int stop = (int)Math.Ceiling ((i + 1) * linepiece);
				for (int j = start; j < stop; j++) {
					int x = i, y = j;

					// if any of the vector's coordinates are negative, offsets change:
					if (v.X < 0)
						x = (-1 * x) - 1;
					if (v.Y < 0)
						y = (-1 * y) - 1;

					yield return new Point (x, y);
				}
			}
		}

    }
}
