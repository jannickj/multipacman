using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.Data;

namespace GooseEngineView.Testing.ConsoleView
{
    public class ConsoleWorldView
    {
        private GooseWorld model;
        Dictionary<Entity, ConsoleEntityView> viewlookup = new Dictionary<Entity, ConsoleEntityView>();

        public ConsoleWorldView(GooseWorld model)
        {
            this.model = model;

        }

        public void AddEntity(ConsoleEntityView entview)
        {
            viewlookup.Add(entview.Model, entview);
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


        public Dictionary<Point, ConsoleEntityView> AllEntities()
        {
            Dictionary<Point, ConsoleEntityView> locs = new Dictionary<Point, ConsoleEntityView>();
            foreach(KeyValuePair<Entity,ConsoleEntityView> kv in this.viewlookup)
                locs.Add(kv.Value.Position,kv.Value);

            return null;
        }


    }
}
