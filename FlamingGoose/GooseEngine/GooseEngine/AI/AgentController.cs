using System;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using System.Threading;

namespace GooseEngine
{
	public abstract class AgentController
	{
		private Agent agent;

		public AgentController (Agent agent)
		{
			this.agent = agent;
		}

		public Agent Target 
		{ 
			get 
			{
				return agent;
			}
		}

		public Percept performAction (GameAction action)
		{
			action.Completed += action_Completed;
			agent.QueueAction (action);

			lock (this) 
			{
				Monitor.Wait(this);
			}

			action.Completed -= action_Completed;
		}

		private void  action_Completed (object sender, EventArgs e)
		{
			lock (this) {
				Monitor.PulseAll(this);
			}
		}
	}
}

