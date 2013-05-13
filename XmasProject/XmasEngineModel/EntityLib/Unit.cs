using System;
using System.Collections.Generic;
using System.Linq;

namespace XmasEngineModel.EntityLib
{
	public abstract class Unit : XmasEntity
	{
		private ICollection<Func<Percept>> perceptCollectors = new List<Func<Percept>>();

		public Unit()
		{
		}

		public ICollection<Percept> Percepts
		{
			get { return perceptCollectors.Select(f => f()).ToArray(); }
		}

		public void AddPerceptCollector(Func<Unit, Percept> f)
		{
			perceptCollectors.Add(() => f(this));
		}
	}
}