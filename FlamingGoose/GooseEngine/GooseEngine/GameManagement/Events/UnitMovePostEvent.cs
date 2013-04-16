using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;

namespace GooseEngine.GameManagement.Events
{
    public class UnitMovePostEvent : GameEvent
    {
		private Point newpos;
		public Point NewPos { 
			get { return newpos; } 
		}

		public UnitMovePostEvent(Point newpos)
		{
			this.newpos = newpos;
		}
    }
}
