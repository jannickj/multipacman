using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model
{
	public class Wall : ConsoleEntity
	{
		public Wall()
		{
			RuleBasedMovementModule module = (RuleBasedMovementModule)this.Module<MovementBlockingModule>();
			module.AddNewRuleLayer<Wall>();
			module.AddWillBlockRule<Wall>(_ => true);
		}
	}
}