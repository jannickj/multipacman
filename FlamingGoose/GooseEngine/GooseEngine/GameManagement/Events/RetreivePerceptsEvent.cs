using System;
using System.Collections.Generic;

namespace GooseEngine.GameManagement.Events
{
	public class RetreivePerceptsEvent : GameEvent
	{
        private ICollection<IPercept> percepts;

        public ICollection<IPercept> Percepts
        {
            get { return percepts; }
        }

		public RetreivePerceptsEvent (ICollection<IPercept> percepts)
		{
			this.percepts = percepts;
		}
	}
}

