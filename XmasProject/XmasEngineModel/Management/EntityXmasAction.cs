using System;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management
{
	public abstract class EntityXmasAction : XmasAction
	{
		private XmasEntity source;

		public XmasEntity Source
		{
			get { return source; }
			internal set { source = value; }
		}

		protected internal abstract Type SupportedEntityType();


		internal bool IsEntitySupported(XmasEntity xmasEntity)
		{
			return xmasEntity.GetType().IsSubclassOf(SupportedEntityType());
		}
	}


	public abstract class EntityXmasAction<T> : EntityXmasAction where T : XmasEntity
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