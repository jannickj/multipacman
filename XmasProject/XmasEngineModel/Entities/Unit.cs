using System;
using System.Collections.Generic;
using System.Linq;

namespace XmasEngineModel.Entities
{
	public abstract class Unit : Entity
	{
		private int health = 1;
		private double moveSpeed = 500;
		private ICollection<Func<Percept>> perceptCollectors = new List<Func<Percept>>();

		public Unit()
		{
			AddRuleSuperior<Unit>();
			AddWillBlock_MovementRule<Unit>(p => p is Unit);
			perceptCollectors.Add(VisionPercept);
			perceptCollectors.Add(HealthPercept);
		}

		public ICollection<Percept> Percepts
		{
			get { return perceptCollectors.Select(f => f()).ToArray(); }
		}

		public int Health
		{
			get { return health; }
			set { health = value; }
		}

		public double MoveSpeed
		{
			get { return moveSpeed; }
			set { moveSpeed = value; }
		}

		public void AddPerceptCollector(Func<Unit, Percept> f)
		{
			perceptCollectors.Add(() => f(this));
		}

		#region BuiltinPerceptCollectors

		private Percept VisionPercept()
		{
			return World.View(this);
		}

		private Percept HealthPercept()
		{
			return Factory.CreateSingleNumeralPercept("health", health);
		}

		#endregion
	}
}