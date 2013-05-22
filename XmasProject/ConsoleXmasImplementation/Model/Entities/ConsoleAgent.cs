using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;

namespace ConsoleXmasImplementation.Model
{
	public abstract class ConsoleAgent : Agent
	{
		public ConsoleAgent(string name) : base(name)
		{
			this.RegisterModule(ConstructSpeedModule());
			this.RegisterModule(new RuleBasedMovementModule());

			RuleBasedMovementModule module = (RuleBasedMovementModule)this.Module<MovementBlockingModule>();
			module.AddNewRuleLayer<ConsoleAgent>();
			module.AddWillBlockRule<ConsoleAgent>(entity => entity is Agent);
		}

		

		protected abstract SpeedModule ConstructSpeedModule();

	}
}
