using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Data;
using XmasEngineModel.World;

namespace XmasEngineModel
{
	public abstract class XmasWorld
	{
		internal protected abstract bool AddEntity(Entity entity, EntitySpawnInformation info);

		public abstract XmasPosition GetEntityPosition(Entity entity);

		public abstract bool SetEntityPosition(Entity entity, XmasPosition tilePosition);
	}
}
