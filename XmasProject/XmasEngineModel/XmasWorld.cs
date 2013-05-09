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
		internal protected abstract void AddEntity(Entity ent, EntitySpawnInformation info);


		public abstract XmasPosition GetEntityPosition(Entity entity);

		public abstract void SetEntityPosition(Entity entity, XmasPosition tilePosition);
	}
}
