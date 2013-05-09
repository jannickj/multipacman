using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data.GenericEvents;

namespace GooseEngine.GameManagement
{
	public class MultiTrigger : Trigger
	{
		private Dictionary<object, Action<GameEvent>> actionDictionary = new Dictionary<object, Action<GameEvent>>();
		private HashSet<Action<GameEvent>> actions = new HashSet<Action<GameEvent>>();
		private Dictionary<object, Predicate<GameEvent>> condDictionary = new Dictionary<object, Predicate<GameEvent>>();
		private HashSet<Predicate<GameEvent>> conditions = new HashSet<Predicate<GameEvent>>();
		private HashSet<Type> eventtypes = new HashSet<Type>();

		public override ICollection<Type> Events
		{
			get { return eventtypes.ToArray(); }
		}

		public ICollection<Predicate<GameEvent>> Conditions
		{
			get { return conditions.ToArray(); }
		}

		public ICollection<Action<GameEvent>> Actions
		{
			get { return actions.ToArray(); }
		}

		internal event UnaryValueHandler<Type> RegisteredEvent;
		internal event UnaryValueHandler<Type> DeregisteredEvent;

		public void RegisterEvent<T>() where T : GameEvent
		{
			Type type = typeof (T);
			eventtypes.Add(type);
			if (RegisteredEvent != null)
				RegisteredEvent(this, new UnaryValueEvent<Type>(type));
		}


		public void DeregisterEvent<T>() where T : GameEvent
		{
			Type type = typeof (T);
			eventtypes.Remove(type);
			if (DeregisteredEvent != null)
				DeregisteredEvent(this, new UnaryValueEvent<Type>(type));
		}

		internal void RemoveAction<T>(Action<T> action) where T : GameEvent
		{
			removeObject(action, actionDictionary, actions);
		}

		internal void AddAction<T>(Action<T> action) where T : GameEvent
		{
			addObject(e => action((T) e), action, actionDictionary, actions);
		}

		internal void AddCondition<T>(Predicate<T> condition) where T : GameEvent
		{
			addObject(e => condition((T) e), condition, condDictionary, conditions);
		}

		internal void RemoveCondition<T>(Predicate<T> condition) where T : GameEvent
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

		internal override bool CheckCondition(GameEvent evt)
		{
			return Conditions.All(C => C(evt));
		}

		internal override void Execute(GameEvent evt)
		{
			foreach (Action<GameEvent> A in Actions)
			{
				A(evt);
			}
		}
	}
}