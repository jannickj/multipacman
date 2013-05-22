using System;
using System.Threading;
using JSLibrary.Data.GenericEvents;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace XmasEngineController.AI
{
	public abstract class AgentController
	{
		private AutoResetEvent actionComplete = new AutoResetEvent(false);
        private Agent agent;
		private PerceptCollection newpercepts;

		public AgentController(Agent agent)
		{
			this.agent = agent;
			agent.Register(new Trigger<RetreivePerceptsEvent>(agent_RetrievePercepts));
		}


        public Agent Agent
        {
            get { return agent; }
        }

		protected event UnaryValueHandler<PerceptCollection> PerceptsRecieved;

		public void performAction(EntityXmasAction action)
		{
			action.Resolved += action_Completed;
			agent.QueueAction(action);

			actionComplete.WaitOne();

			PerceptCollection activepercepts = null;
			lock (this)
			{
				activepercepts = newpercepts;
				newpercepts = null;
			}

			if (PerceptsRecieved != null && activepercepts != null && action.ActionFailed == false)
			{
				PerceptsRecieved(this, new UnaryValueEvent<PerceptCollection>(activepercepts));
			}
		}


		public abstract void Start();

		#region EVENTS

		private void action_Completed(object sender, EventArgs e)
		{
			actionComplete.Set();
			((XmasAction) sender).Resolved -= action_Completed;
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
			get { return agent; }
		}

		#endregion
	}
}