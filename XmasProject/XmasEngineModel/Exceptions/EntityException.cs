using System;

namespace XmasEngineModel.Exceptions
{
	public class EntityException : Exception
	{
		private Entity entity;

		public EntityException(Entity entity)
		{
			// TODO: Complete member initialization
			this.entity = entity;
		}

		public EntityException(Entity e, string msg) : base(msg)
		{
			entity = e;
		}

		public Entity Entity
		{
			get { return entity; }
		}
	}
}