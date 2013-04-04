using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Data
{
    public struct Size
    {
        private int width;
        private int height;

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;

        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

    }
}
