using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Data
{
    public struct Vector
    {
        private int x;
        private int y;
        
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;

        }

		public Vector(Point origin, Point destination)
		{
			x = destination.X - origin.X;
			y = destination.Y - origin.Y;
		}

        public int X
        {
            get { return x; }
        }


        public int Y
        {
            get { return y; }
        }

        public Point ToPoint()
        {
            return new Point(x, y);
        }
    }
}
