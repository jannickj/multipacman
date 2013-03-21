using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.ActionManagement;

namespace GooseEngine
{
    public class ActionHandler
    {
        private GameWorld world;

        public ActionHandler(GameWorld world)
        {
            // TODO: Complete member initialization
            this.world = world;
            
        }


        public void Enqueue(Entity entity, GameAction action)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
