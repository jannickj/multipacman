using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using XmasEngineController.AI;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

namespace ConsoleXmasImplementation.Controller
{
	public class HumanInterfaceManager : AgentManager
	{
		public HumanInterfaceManager()
		{

		}

		public override void Initialize()
		{
			this.EventManager.Register(new Trigger<EntityAddedEvent>(game_EntityAdded));
		}

		protected override Func<KeyValuePair<string, AgentController>> AquireAgentControllerContructor()
		{
			return AgentControllerConstructor;
		}

		private KeyValuePair<string, AgentController> AgentControllerConstructor()
		{
			Agent player = this.TakeControlOf("player");
			var kv = new KeyValuePair<string, AgentController>(player.Name,new HumanInterfaceController(player));

			return kv;
		}

		private void game_EntityAdded(EntityAddedEvent evt)
		{
			if (evt.AddedXmasEntity is Player)
			{
				this.AddAgent(evt.AddedXmasEntity as Player);
			}
		}

	}
}
