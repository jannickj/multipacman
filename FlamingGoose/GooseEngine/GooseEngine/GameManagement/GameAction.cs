﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.ActionManagement;
using GooseEngine.Interfaces;

namespace GooseEngine.GameManagement
{
    public abstract class GameAction
    {
        private GameWorld world;

        public GameAction()
        { 

        }

        public event EventHandler Completed;

        internal void Fire(IGameManager gem)
        {
            this.Execute(gem);
        }

        protected abstract void Execute(IGameManager gem);

        protected void Complete()
        {
            if (Completed != null)
                Completed(this, new EventArgs());
        }


        internal GameWorld World
        {
            get
            {
                return world;
            }
            set
            {
                this.world = value;
            }
        }
    }
}
