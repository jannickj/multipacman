using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GooseEngine.Data;

namespace GooseEngineView.Testing.ConsoleView
{
    public class GooseConsoleView
    {

        private ConsoleWorldView world;
        private GooseEngine.GameWorld gameWorld;

        public GooseConsoleView(GooseEngine.GameWorld gameWorld)
        {
            // TODO: Complete member initialization
            this.gameWorld = gameWorld;
        }

        public void Setup()
        {
            Console.SetWindowSize(world.Width, world.Height);
            Timer timer = new Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 1000 / 25;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            draw();
        }

        private void draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(Area());
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
