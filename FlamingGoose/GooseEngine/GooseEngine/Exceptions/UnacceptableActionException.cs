using System;
using GooseEngine.GameManagement;

namespace GooseEngine.Exceptions
{
	public class UnacceptableActionException : Exception
	{
		private GameAction action;
		private Entity entity;

		public UnacceptableActionException(GameAction action, Entity entity)
			: base("Entity: [" + entity.GetType().Name + "] can't accept action: [" + action.GetType().Name + "]")
		{
			this.action = action;
			this.entity = entity;
		}

		public GameAction Action
		{
			get { return action; }
		}

		public Entity Entity
		{
			get { return entity; }
		}
	}
}