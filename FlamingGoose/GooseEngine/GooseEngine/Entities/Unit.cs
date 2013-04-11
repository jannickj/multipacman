using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooseEngine.Entities.Interactables;

namespace GooseEngine.Entities
{
    public abstract class Unit : Entity
    {
		private ICollection<Func<IPercept>> perceptCollectors = new List<Func<IPercept>> ();
        private int health = 1;

		public ICollection<IPercept> Percepts {
			get {
				return perceptCollectors.Select (f => f()).ToArray();
			}
		}

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Unit()
        {
            this.AddRuleSuperior<Unit>();
            AddWillBlock_MovementRule<Unit>(p => p is Unit);
        	
        }

		public void AddPerceptCollector (Func<Unit, IPercept> f)
		{
			perceptCollectors.Add (() => f (this));
		}

		#region BuiltinPerceptCollectors

		private IPercept VisionPercept ()
		{
			return World.View(this);
		}

		private IPercept HealthPercept ()
		{
			return Factory.CreateSingleNumeralPercept ("health", this.health);
		}

		#endregion
        
    }
}