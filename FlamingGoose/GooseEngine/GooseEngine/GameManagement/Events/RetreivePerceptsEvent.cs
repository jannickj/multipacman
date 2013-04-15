using System;
using System.Collections.Generic;

namespace GooseEngine.GameManagement.Events
{
	public class RetreivePerceptsEvent : GameEvent
	{
        private PerceptCollection percepts;

        public PerceptCollection Percepts
        {
            get { return percepts; }
        }

		public RetreivePerceptsEvent (PerceptCollection percepts)
		{
			this.percepts = percepts;
		}
	}
}

