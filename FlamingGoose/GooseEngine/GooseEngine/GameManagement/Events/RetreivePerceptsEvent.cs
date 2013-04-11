using System;
using System.Collections.Generic;

namespace GooseEngine.GameManagement.Events
{
	public class RetreivePerceptsEvent : GameEvent
	{
		private ICollection<IPercept> percepts;

		public RetreivePerceptsEvent (ICollection<IPercept> percepts)
		{
			this.percepts = percepts;
		}
	}
}

