using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model
{
	public abstract class ConsoleEntity : XmasEntity
	{
		public ConsoleEntity()
		{
			this.RegisterModule(new RuleBasedMovementModule());
			RuleBasedMovementModule module = (RuleBasedMovementModule)this.Module<MovementBlockingModule>();
			module.AddNewRuleLayer<ConsoleEntity>();
		}

	}
}
