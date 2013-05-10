namespace XmasEngineModel.World
{
	public abstract class EntitySpawnInformation
	{
		private XmasPosition pos;

		public EntitySpawnInformation(XmasPosition pos)
		{
			this.pos = pos;
		}

		public XmasPosition Position
		{
			get { return pos; }
		}
	}
}