using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model
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