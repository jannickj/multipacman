using System;

namespace GooseEngine.Data
{
	public struct Point 
	{
		private int x, y;

		int X { get { return x; } }
		int Y { get { return y; } }

		public Point (int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Point (Point p, Vector v)
		{
			x = p.X + v.X;
			y = p.Y + v.Y;
		}

		public static Point operator +(Point p1, Point p2)
		{
			return new Point (p1.X + p2.X, p1.Y + p2.Y);
		}

		public static Point operator -(Point p1, Point p2)
		{
			return new Point (p1.X - p2.X, p1.Y - p2.Y);
		}

		public static Point operator +(Point p, Vector v)
		{
			return new Point (p.X - v.X, p.Y - v.Y);
		}
	}
}

