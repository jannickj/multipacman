using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions.TileExtension.Entities
{
	public class Wall : XmasEntity
	{
		public Wall()
		{
			AddRuleSuperior<Wall>();
			AddWillBlock_MovementRule<Wall>(_ => true);
		}

		public override bool IsVisionBlocking(XmasEntity xmasEntity)
		{
			return true;
		}
	}
}