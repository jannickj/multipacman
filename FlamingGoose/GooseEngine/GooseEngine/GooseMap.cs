using System;
using System.Collections.Generic;
using System.Linq;
using GooseEngine.Entities;
using JSLibrary.Data;

namespace GooseEngine
{
	public class GooseMap
	{
		private Size burstSize;
		private Point center;
		private Tile outofmaptile = new Tile();
		private Size size;
		private Tile[,] tiles;

		public GooseMap(Size burstSize)
		{
			outofmaptile.AddEntity(new ImpassableWall());
			this.burstSize = burstSize;
			size = new Size(burstSize.Width*2 + 1, burstSize.Height*2 + 1);
			center = new Point(burstSize.Width, burstSize.Height);
			tiles = new Tile[size.Width,size.Height];

			for (int i = 0; i < size.Width; i++)
			{
				for (int j = 0; j < size.Height; j++)
				{
					tiles[i, j] = new Tile();
				}
			}
		}

		public Size Size
		{
			get { return size; }
		}

		public Tile this[int x, int y]
		{
			get
			{
				if (IsOutOfBounds(x, y))
				{
					return outofmaptile;
				}
				return tiles[center.X + x, center.Y - y];
			}
			set
			{
				if (!IsOutOfBounds(x, y))
					tiles[center.X + x, center.Y - y] = value;
			}
		}

		public Grid<Tile> this[int x, int y, int range]
		{
			get
			{
				int startx = x - range;
				int starty = y - range;
				int rsize = range*2 + 1;

				Tile[,] r = new Tile[rsize,rsize];

				for (int i = 0; i < rsize; i++)
				{
					for (int j = 0; j < rsize; j++)
					{
						r[i, j] = this[i + startx, j + starty];
					}
				}

				int gridc = rsize/2;

				Grid<Tile> grid = new Grid<Tile>(r, new Point(gridc, gridc));

				return grid;
			}
		}

		private bool IsOutOfBounds(int x, int y)
		{
			return (x > burstSize.Width || x < -burstSize.Width || y > burstSize.Height || y < -burstSize.Height);
		}

		public IEnumerable<Tile> TilesInChunk(Point start, Point stop)
		{
			Point min = new Point(Math.Min(start.X, stop.X), Math.Min(start.Y, stop.Y));
			Point max = new Point(Math.Max(start.X, stop.X), Math.Max(start.Y, stop.Y));

			for (int x = min.X; x <= max.X; x++)
				for (int y = min.Y; y <= max.Y; y++)
					yield return this[x, y];
		}

		public void AddChunk<EntityType>(Point start, Point stop)
			where EntityType : Entity, new()
		{
			foreach (Tile tile in TilesInChunk(start, stop))
			{
				EntityType entity = new EntityType();
				if (tile.CanContain(entity))
					tile.AddEntity(entity);
			}
		}

		public void RemoveChunk<EntityType>(Point start, Point stop)
			where EntityType : Entity, new()
		{
			foreach (Tile tile in TilesInChunk(start, stop))
			{
				foreach (EntityType entity in tile.Entities.OfType<EntityType>())
					tile.RemoveEntity(entity);
			}
		}
	}
}