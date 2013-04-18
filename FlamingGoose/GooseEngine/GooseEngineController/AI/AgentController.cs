using System;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data.GenericEvents;
using GooseEngine;

namespace GooseEngineController.AI
{
	public abstract class AgentController
	{
		private Agent agent;
        protected event UnaryValueHandler<PerceptCollection> PerceptsRecieved;
        private PerceptCollection newpercepts = null;
        private AutoResetEvent actionComplete = new AutoResetEvent(false);

		public AgentController (Agent agent)
		{
			this.agent = agent;
            agent.Register(new Trigger<RetreivePerceptsEvent>(agent_RetrievePercepts));
		}

		public void performAction (EntityGameAction action)
		{
			action.Completed += action_Completed;
			agent.QueueAction(action);

            this.actionComplete.WaitOne();

            PerceptCollection activepercepts = null;
            lock (this)
            {
                activepercepts = this.newpercepts;
                this.newpercepts = null;
            }

            if (PerceptsRecieved != null && activepercepts != null)
            {
                this.PerceptsRecieved(this, new UnaryValueEvent<PerceptCollection>(activepercepts));
            }
			
		}

        

        public abstract void Start();

        #region EVENTS
        private void  action_Completed (object sender, EventArgs e)
		{
            this.actionComplete.Set();
        }

        private void agent_RetrievePercepts(RetreivePerceptsEvent e)
        {
            lock (this)
            {
                newpercepts = e.Percepts;
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

