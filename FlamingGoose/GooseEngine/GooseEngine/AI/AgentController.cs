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


		public void performAction (GameAction action)
		{
			action.Completed += action_Completed;
			agent.QueueAction (action);

			lock (this) 
			{
				Monitor.Wait(this);
			}
		}



		#region Events
		private void  action_Completed (object sender, EventArgs e)
		{
			lock (this) 
			{
				Monitor.PulseAll (this);
			}
		}
		#endregion

		#region Getters
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

