using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions.TileExtension.Entities
{
	public class ImpassableWall : XmasEntity
	{
		public ImpassableWall()
		{
			AddRuleSuperior<ImpassableWall>();
			AddWillBlock_MovementRule<ImpassableWall>(_ => true);
		}

		public override bool IsVisionBlocking(XmasEntity xmasEntity)
		{
			return true;
		}
	}
}