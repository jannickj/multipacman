using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine;
using GooseEngine.ActionManagement;

namespace GameEngine.ActionManagement
{
    public class GameEventManager
    {

        public void Listen<T>(Type EventType, Entity specific, Action<T> listener) where T : GameEvent
        {
            
            throw new NotImplementedException();
        }

        public void Listen<T>(Type EventType, Action<T> listener) where T : GameEvent
        {

            throw new NotImplementedException();
        }

        internal void Raise(GameEvent evt)
        {
            throw new NotImplementedException();
        }

        public void Queue(GameAction action)
        {
            throw new NotImplementedException();
        }
    }
}
