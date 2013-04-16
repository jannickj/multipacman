using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;

namespace GooseEngineView.Testing.ConsoleView
{
    class ConsoleWorldView
    {
        private GameWorld model;

        

        public ConsoleWorldView(GameWorld model)
        {
            this.model = model;
        }



        internal char[] GenerateDraw()
        {
            throw new NotImplementedException();
        }

        public int Width
        {
            get
            {
                return model.Size.Width;
            }
        }

        public int Height
        {
            get
            {
                return model.Size.Height;
            }
        }


        internal Dictionary<GooseEngine.Data.Point, ConsoleEntityView> AllEntities()
        {
            throw new NotImplementedException();
        }


    }
}
