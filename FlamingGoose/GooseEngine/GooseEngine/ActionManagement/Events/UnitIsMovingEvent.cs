using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.ActionManagement.Events
{
    public class UnitIsMovingEvent : GameEvent
    {
        public bool IsInterrupted { get; set; }
    }
}
