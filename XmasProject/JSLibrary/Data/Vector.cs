using System;

namespace JSLibrary.Data
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

		public Vector Direction
		{
			get
			{
				int newX = x == 0 ? 0 : x/Math.Abs(x);
				int newY = y == 0 ? 0 : y/Math.Abs(y);

				return new Vector(newX, newY);
			}
		}
		public override string ToString ()
		{
			return string.Format ("<{0},{1}>", X, Y);
		}
	}
}