using System;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data.GenericEvents;

namespace GooseEngine
{
	public abstract class AgentController
	{
		private Agent agent;
        protected event ValueHandler<ICollection<IPercept>> PerceptsRecieved;
        private ICollection<IPercept> newpercepts = null;

		public AgentController (Agent agent)
		{
			this.agent = agent;
            agent.Register(new Trigger<RetreivePerceptsEvent>(agent_RetrievePercepts));
		}

		public void performAction (EntityGameAction action)
		{
			action.Completed += action_Completed;
			agent.QueueAction(action);
           
			lock (this) 
			{
				Monitor.Wait(this);
            }

            ICollection<IPercept> activepercepts = null;
            lock (this)
            {
                activepercepts = this.newpercepts;
                this.newpercepts = null;
            }

            if (PerceptsRecieved != null && activepercepts != null)
            {
                this.PerceptsRecieved(this, new ValueEvent<ICollection<IPercept>>(newpercepts));
            }
			
		}

        

        public abstract void Start();

        #region EVENTS
        private void  action_Completed (object sender, EventArgs e)
		{
			lock (this) {
				Monitor.PulseAll(this);
			}
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

