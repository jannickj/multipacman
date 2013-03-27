﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooseEngine.Data
{
    public struct Grid<T>
    {
        private T[,] data;
        private Size size;
		public Point Center { get; private set; }

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
            Center = center;
            size = new Size(data.GetLength(0), data.GetLength(1));
           
        }
    }
}
