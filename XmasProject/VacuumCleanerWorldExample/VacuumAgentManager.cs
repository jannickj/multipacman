using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmasEngineController.AI;
using XmasEngineModel.EntityLib;

namespace VacuumCleanerWorldExample
{
	public class VacuumAgentManager : AgentManager
	{
		private int constructedAgents = 0;
		private string name;

		//Provide the manager with the name of the agent it wishes to construct an agentcontroller for
		public VacuumAgentManager(string name)
		{
			this.name = name;
		}

		//Override this method which will be called repeatably once the engine starts
		protected override Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor()
		{
			//Stop the AgentManager thread once the vacuum cleaner constroller has been constructed
			if (constructedAgents > 0)
				Thread.Sleep(Timeout.InfiniteTimeSpan);
			constructedAgents++;

			//The name of the agent the manager will try to locate
			

			Agent agent = this.TakeControlOf(name);

			//Lambda function that constructs new agent controllers, this constructor will be called in the agent controller's own thread
			Func<KeyValuePair<string,AgentController>> LambdaConstructor;
			
			LambdaConstructor = () => new KeyValuePair<string,AgentController>(name,new VacuumAgentController(agent));
			
			return LambdaConstructor;
		}
	}
}
