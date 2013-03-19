using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;

namespace GooseEngine
{
    public class GameMap
    {
        private Tile[,] tiles;
        private Point center;
        private Size size;
        private Size burstSize;

        public GameMap(Size burstSize)
        {
            this.burstSize = burstSize;
            size = new Size(burstSize.Width * 2 + 1, burstSize.Height * 2 + 1);
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

        public Tile this[int x, int y]
        {
            get
            {
                if(center.X + x > burstSize.Width || center.X + x < 0 || center.Y + y > burstSize.Height || center.Y + y < 0 )
                {
                    Tile t = new Tile();
                    t.AddEntity(new ImpassableWall());
                    return t;
                }
                return tiles[center.X + x, center.Y + y];
            }
            set
            {
                tiles[center.X + x, center.Y + y] = value;
            }
        }

        public Grid<Tile> this[int x, int y, int range]
        {
            get
            {
                int startx = center.X - range;
                //startx = startx < 0 ? 0 : startx;
                
                int starty = center.Y - range;
                //starty = starty < 0 ? 0 : starty;

                int rsize = range * 2 + 1;
                //int rsizeX = rsize > burstSize.Width + x ? burstSize.Width + x : rsize;
                //int rsizeY = rsize > burstSize.Height + y ? burstSize.Height + y : rsize;

                Tile[,] r = new Tile[rsize, rsize];

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
    }
}
