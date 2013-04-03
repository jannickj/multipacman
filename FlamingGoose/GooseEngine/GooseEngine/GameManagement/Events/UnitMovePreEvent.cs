using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement.Events
{
    public class UnitMovePreEvent : GameEvent
    {
        public bool IsStopped { get; set; }
    }
}
