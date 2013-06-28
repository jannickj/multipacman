using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacuumCleanerWorldExample.Modules;
using XmasEngineModel.EntityLib;

namespace VacuumCleanerWorldExample
{
	public class VacuumCleanerAgent : Agent
	{
		public VacuumCleanerAgent(string name)
			: base(name)
		{
			this.RegisterModule(new VacuumVisionModule());
			
		}
	}
}
