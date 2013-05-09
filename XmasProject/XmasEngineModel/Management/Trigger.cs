using System;
using System.Collections.Generic;
using XmasEngineModel.Management.Interfaces;

namespace XmasEngineModel.Management
{
	public abstract class Trigger : ITrigger
	{
		public abstract ICollection<Type> Events { get; }

		internal abstract bool CheckCondition(XmasEvent evt);

		internal abstract void Execute(XmasEvent evt);
	}

	public class Trigger<T> : Trigger where T : XmasEvent
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

		internal override bool CheckCondition(XmasEvent evt)
		{
			return condition((T) evt);
		}

		internal override void Execute(XmasEvent evt)
		{
			action((T) evt);
		}
	}
}