using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngine.Exceptions
{
	public class EngineAlreadyStartedException : Exception
	{
		public EngineAlreadyStartedException()
			: base("Engine was already started, stop the engine before starting it up again")
		{
			
		}
	}
}
