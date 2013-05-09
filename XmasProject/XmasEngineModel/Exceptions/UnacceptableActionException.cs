using System;
using XmasEngineModel.Management;

namespace XmasEngineModel.Exceptions
{
	public class UnacceptableActionException : Exception
	{
		private XmasAction action;
		private Entity entity;

		public UnacceptableActionException(XmasAction action, Entity entity)
			: base("Entity: [" + entity.GetType().Name + "] can't accept action: [" + action.GetType().Name + "]")
		{
			this.action = action;
			this.entity = entity;
		}

		public XmasAction Action
		{
			get { return action; }
		}

		public Entity Entity
		{
			get { return entity; }
		}
	}
}