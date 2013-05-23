using System;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension.Modules;

namespace ConsoleXmasImplementation.Model.Entities
{
	public class DropZone : ConsoleEntity
	{
		public DropZone ()
		{
			RuleBasedMovementBlockingModule blockingModule = (RuleBasedMovementBlockingModule)this.Module<MovementBlockingModule>();
			blockingModule.AddNewRuleLayer<DropZone>();
			blockingModule.AddWillNotBlockRule<DropZone>(_ => true);
		}
	}
}

