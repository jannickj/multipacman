using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Modules;

namespace XmasEngineExtensions.TileExtension.Entities
{
	public class ImpassableWall : XmasEntity
	{
		public ImpassableWall()
		{
			RegisterModule (new RuleBasedMovementModule());
			RuleBasedMovementModule movement = (RuleBasedMovementModule)this.Module<MovementBlockingModule>();
			movement.AddNewRuleLayer<ImpassableWall>();
			movement.AddWillBlockRule<ImpassableWall>(_ => true);
		}
	}
}