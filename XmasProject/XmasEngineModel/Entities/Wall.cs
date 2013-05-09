namespace XmasEngineModel.Entities
{
	public class Wall : Entity
	{
		public Wall()
		{
			AddRuleSuperior<Wall>();
			AddWillBlock_MovementRule<Wall>(_ => true);
		}

		public override bool IsVisionBlocking(Entity entity)
		{
			return true;
		}
	}
}