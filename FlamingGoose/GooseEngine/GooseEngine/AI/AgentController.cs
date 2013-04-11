using System;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace GooseEngine
{
	public abstract class AgentController
	{
		private Agent agent;

		public AgentController (Agent agent)
		{
			this.agent = agent;
		}

		public void performAction (EntityGameAction action)
		{
			action.Completed += action_Completed;
			agent.QueueAction(action);
           
			lock (this) 
			{
				Monitor.Wait(this);
			}

			action.Completed -= action_Completed;
		}

        #region EVENTS
        private void  action_Completed (object sender, EventArgs e)
		{
			lock (this) {
				Monitor.PulseAll(this);
			}
        }
        #endregion

        #region GETTERS
        public Agent Target
        {
            get
            {
                return agent;
            }
        }
        #endregion
    }
}

