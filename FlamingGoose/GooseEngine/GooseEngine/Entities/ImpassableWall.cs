namespace GooseEngine.Entities
{
	public class ImpassableWall : Entity
	{
		public ImpassableWall()
		{
			AddRuleSuperior<ImpassableWall>();
			AddWillBlock_MovementRule<ImpassableWall>(_ => true);
		}

		public override bool IsVisionBlocking(Entity entity)
		{
			return true;
		}
	}
}