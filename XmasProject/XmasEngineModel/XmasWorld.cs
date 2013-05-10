using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace XmasEngineModel
{
	public abstract class XmasWorld
	{
		protected internal abstract bool AddEntity(XmasEntity xmasEntity, EntitySpawnInformation info);

		public abstract XmasPosition GetEntityPosition(XmasEntity xmasEntity);

		public abstract bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition);
	}
}