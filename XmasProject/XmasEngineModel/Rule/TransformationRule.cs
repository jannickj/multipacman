using System;
using System.Collections.Generic;
using System.Linq;
using XmasEngineModel.Rule.Exceptions;

namespace XmasEngineModel.Rule
{
	public class TransformationRule<T>
	{
		private Dictionary<Conclusion, LinkedList<Predicate<T>>> premises =
			new Dictionary<Conclusion, LinkedList<Predicate<T>>>();

		public void AddPremise(Predicate<T> p, Conclusion c)
		{
			LinkedList<Predicate<T>> list;
			if (!premises.TryGetValue(c, out list))
			{
				list = new LinkedList<Predicate<T>>();
				premises.Add(c, list);
			}

			list.AddFirst(p);
		}

		public Conclusion Conclude(T t)
		{
			ICollection<Conclusion> cons = MultiConcluding(t);
			if (cons.Count > 1)
				throw new MultiConclusionException(cons.ToArray());
			if (cons.Count == 0)
				return new DontCareConclusion();
			return cons.First();
		}

		public ICollection<Conclusion> MultiConcluding(T t)
		{
			return premises.Where(kp => kp.Value.Any(p => p(t))).Select(kp => kp.Key).ToArray();
		}
	}
}