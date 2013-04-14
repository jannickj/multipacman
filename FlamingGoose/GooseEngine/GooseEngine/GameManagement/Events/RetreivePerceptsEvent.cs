using System;
using System.Collections.Generic;

namespace GooseEngine.GameManagement.Events
{
	public class RetreivePerceptsEvent : GameEvent
	{
        private ICollection<Percept> percepts;

        public ICollection<Percept> Percepts
        {
            get { return percepts; }
        }

		public RetreivePerceptsEvent (ICollection<Percept> percepts)
		{
			this.percepts = percepts;
		}
	}
}

