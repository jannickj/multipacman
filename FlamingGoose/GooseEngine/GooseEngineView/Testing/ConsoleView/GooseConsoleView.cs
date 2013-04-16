using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;

namespace GooseEngineView.Testing.ConsoleView
{
    public class GooseConsoleView
    {

        private ConsoleWorldView world;

        public void Setup()
        {
            Console.SetWindowSize(world.Width, world.Height);
        }

        private void draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(Area());
        }

        public void Start()
        {
            while (true)
            {
                draw();
            }
        }

        public Char[] Area()
        {
            Dictionary<Point,ConsoleEntityView> entities = world.AllEntities();
            char[] drawchars = new char[world.Height * world.Width + world.Height];

            for (int i = 0; i < world.Height*world.Width; i++)
            {
                drawchars[i] = ' ';
            }
            for (int i = 0; i < world.Height; i++)
            {
                drawchars[world.Width + i * world.Height] = '\n';
            }

            foreach (KeyValuePair<Point, ConsoleEntityView> kv in entities)
            {
                int x = kv.Key.X;
                int y = kv.Key.Y;
                int cord = x + y*world.Width;

                drawchars[cord] = kv.Value.Symbol;
            }
            return drawchars;
        }
    }
}
