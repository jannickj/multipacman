using System;

namespace XmasEngineModel.Management
{
	public abstract class EntityXmasAction : XmasAction
	{
		private Entity source;

		public Entity Source
		{
			get { return source; }
			internal set { source = value; }
		}

		protected internal abstract Type SupportedEntityType();


		internal bool IsEntitySupported(Entity entity)
		{
			return entity.GetType().IsSubclassOf(SupportedEntityType());
		}
	}


	public abstract class EntityXmasAction<T> : EntityXmasAction where T : Entity
	{
		public new T Source
		{
			get { return (T) base.Source; }
		}


		protected internal override Type SupportedEntityType()
		{
			return typeof (T);
		}
	}
}