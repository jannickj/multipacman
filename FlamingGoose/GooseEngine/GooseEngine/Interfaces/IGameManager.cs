using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.GameManagement;

namespace GooseEngine.Interfaces
{
    public interface IGameManager
    {
        void Raise(GameEvent evt);

        void AddEntity(Entity entity);

        void RemoveEntity(Entity entity);

        void Queue(GameAction action);

        void Register(Trigger trigger);

        void Deregister(Trigger trigger);

        GameTimer CreateTimer(Action action);
    }
}
