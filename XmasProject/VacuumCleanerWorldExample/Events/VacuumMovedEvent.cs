using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmasEngineModel.Management;
using XmasEngineModel.World;

namespace VacuumCleanerWorldExample.Events
{
	public class VacuumMovedEvent : XmasEvent
	{
		public XmasPosition From{get; private set;}
		public XmasPosition To { get; private set; }

		public VacuumMovedEvent(XmasPosition from, XmasPosition to)
		{
			this.From = from;
			this.To = to;
		}
	}
}
