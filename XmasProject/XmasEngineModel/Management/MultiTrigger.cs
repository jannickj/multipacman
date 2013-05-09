using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data.GenericEvents;

namespace XmasEngineModel.Management
{
	public class MultiTrigger : Trigger
	{
		private Dictionary<object, Action<XmasEvent>> actionDictionary = new Dictionary<object, Action<XmasEvent>>();
		private HashSet<Action<XmasEvent>> actions = new HashSet<Action<XmasEvent>>();
		private Dictionary<object, Predicate<XmasEvent>> condDictionary = new Dictionary<object, Predicate<XmasEvent>>();
		private HashSet<Predicate<XmasEvent>> conditions = new HashSet<Predicate<XmasEvent>>();
		private HashSet<Type> eventtypes = new HashSet<Type>();

		public override ICollection<Type> Events
		{
			get { return eventtypes.ToArray(); }
		}

		public ICollection<Predicate<XmasEvent>> Conditions
		{
			get { return conditions.ToArray(); }
		}

		public ICollection<Action<XmasEvent>> Actions
		{
			get { return actions.ToArray(); }
		}

		internal event UnaryValueHandler<Type> RegisteredEvent;
		internal event UnaryValueHandler<Type> DeregisteredEvent;

		public void RegisterEvent<T>() where T : XmasEvent
		{
			Type type = typeof (T);
			eventtypes.Add(type);
			if (RegisteredEvent != null)
				RegisteredEvent(this, new UnaryValueEvent<Type>(type));
		}


		public void DeregisterEvent<T>() where T : XmasEvent
		{
			Type type = typeof (T);
			eventtypes.Remove(type);
			if (DeregisteredEvent != null)
				DeregisteredEvent(this, new UnaryValueEvent<Type>(type));
		}

		internal void RemoveAction<T>(Action<T> action) where T : XmasEvent
		{
			removeObject(action, actionDictionary, actions);
		}

		internal void AddAction<T>(Action<T> action) where T : XmasEvent
		{
			addObject(e => action((T) e), action, actionDictionary, actions);
		}

		internal void AddCondition<T>(Predicate<T> condition) where T : XmasEvent
		{
			addObject(e => condition((T) e), condition, condDictionary, conditions);
		}

		internal void RemoveCondition<T>(Predicate<T> condition) where T : XmasEvent
		{
			removeObject(condition, condDictionary, conditions);
		}

		private void addObject<T>(T wrapper, object o, IDictionary<object, T> dic, ICollection<T> list)
		{
			dic.Add(o, wrapper);
			list.Add(wrapper);
		}

		private void removeObject<T>(object o, IDictionary<object, T> dic, HashSet<T> list)
		{
			T wrapper;
			if (!dic.TryGetValue(o, out wrapper))
				return;

			dic.Remove(o);

			list.Remove(wrapper);
		}

		internal override bool CheckCondition(XmasEvent evt)
		{
			return Conditions.All(C => C(evt));
		}

		internal override void Execute(XmasEvent evt)
		{
			foreach (Action<XmasEvent> A in Actions)
			{
				A(evt);
			}
		}
	}
}