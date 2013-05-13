using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Modules;

namespace XmasEngineExtensions.TileExtension.Entities
{
	public class ImpassableWall : XmasEntity
	{
		public ImpassableWall()
		{
			RegisterModule (new RuleBasedMovementModule ());
		}
	}
}