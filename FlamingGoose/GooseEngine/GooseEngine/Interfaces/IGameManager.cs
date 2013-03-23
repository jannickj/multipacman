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
    }
}
