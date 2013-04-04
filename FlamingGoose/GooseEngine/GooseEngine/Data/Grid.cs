using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Data
{
    public struct Grid<T>
    {
        private T[,] data;
        private Size size;
        private Point center;
       
		public Point Center 
        {
            get
            {
                return center;
            }
        }

        public T this[int x, int y]
        {
            get
            {
                return data[x, y];
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }
        }

        public Grid(T[,] data, Point center)
        {
            this.data = data;
            this.center = center;
            size = new Size(data.GetLength(0), data.GetLength(1));
           
        }

		public IEnumerable<Point> getAdjacent(Point p)
		{
			int startX = p.X - 1, 
				startY = p.Y - 1, 
				stopX  = p.X + 1, 
				stopY  = p.X + 1;

			if (p.X == 0) 
				startX = 0;
			if (p.X == size.Width)
				startX = size.Width - 1;

			if (p.Y == 0)
				startY = 0;
			if (p.Y == size.Height)
				startY = size.Height - 1;

			for (int x = startX; x <= stopX; x++)
				for (int y = startY; y <= stopY; y++)
					yield return new Point (x, y);
		}
    }
}
