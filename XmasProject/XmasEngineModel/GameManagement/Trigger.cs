using System;
using System.Collections.Generic;
using XmasEngineModel.GameManagement.Interfaces;

namespace XmasEngineModel.GameManagement
{
	public abstract class Trigger : ITrigger
	{
		public abstract ICollection<Type> Events { get; }

		internal abstract bool CheckCondition(GameEvent evt);

		internal abstract void Execute(GameEvent evt);
	}

	public class Trigger<T> : Trigger where T : GameEvent
	{
		private Action<T> action;
		private Predicate<T> condition;
		private Type evt = typeof (T);

		public Trigger(Action<T> action)
		{
			this.action = action;
			condition = (_ => true);
		}

		public Trigger(Predicate<T> condition, Action<T> action)
		{
			this.condition = condition;
			this.action = action;
		}

		public override ICollection<Type> Events
		{
			get { return new[] {evt}; }
		}

		internal override bool CheckCondition(GameEvent evt)
		{
			return condition((T) evt);
		}

		internal override void Execute(GameEvent evt)
		{
			action((T) evt);
		}
	}
}