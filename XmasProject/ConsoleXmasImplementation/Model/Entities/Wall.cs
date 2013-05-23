﻿using XmasEngineExtensions.TileExtension.Modules;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class Wall : ConsoleEntity
	{
		public Wall()
		{
			RuleBasedMovementBlockingModule blockingModule = (RuleBasedMovementBlockingModule)this.Module<MovementBlockingModule>();
			blockingModule.AddNewRuleLayer<Wall>();
			blockingModule.AddWillBlockRule<Wall>(_ => true);
		}
	}
}