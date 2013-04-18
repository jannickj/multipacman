using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GooseEngine;
using GooseEngine.Data;

namespace GooseEngineView.Testing.ConsoleView
{
    public class GooseConsoleView
    {

        private ConsoleWorldView viewWorld;

        public GooseConsoleView(ConsoleWorldView viewWorld)
        {
			this.viewWorld = viewWorld;
        }

        public void Setup()
        {
			Console.SetWindowSize(viewWorld.Width, viewWorld.Height);
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
			Dictionary<Point, ConsoleEntityView> entities = viewWorld.AllEntities();
			char[] drawchars = new char[viewWorld.Height * viewWorld.Width + viewWorld.Height];

			for (int i = 0; i < viewWorld.Height * viewWorld.Width; i++)
            {
                drawchars[i] = ' ';
            }
			for (int i = 0; i < viewWorld.Height; i++)
            {
				drawchars[viewWorld.Width + i * viewWorld.Height] = '\n';
            }

            foreach (KeyValuePair<Point, ConsoleEntityView> kv in entities)
            {
                int x = kv.Key.X;
                int y = kv.Key.Y;
				int cord = x + y * viewWorld.Width;

                drawchars[cord] = kv.Value.Symbol;
            }
            return drawchars;
        }
    }
}
