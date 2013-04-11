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
		private ICollection<Func<Percept>> perceptCollectors = new List<Func<Percept>> ();
        private int health = 1;

		public ICollection<Percept> Percepts {
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

		public void AddPerceptCollector (Func<Unit, Percept> f)
		{
			perceptCollectors.Add (p => f (this));
		}

		#region BuiltinPerceptCollectors

		private Percept VisionPerceptCollector ()
		{
			return World.View(this);
		}

		#endregion
        
    }
}
