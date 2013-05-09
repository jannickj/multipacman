using GooseEngine.Entities;

namespace GooseEngine.GameManagement.Events
{
	public class UnitTakesDamagePostEvent : GameEvent
	{
		private int actualDmg;
		private int dmg;
		private Unit source;
		private Unit target;

		public UnitTakesDamagePostEvent(Unit source, Unit target, int dmg, int actualDmg)
		{
			// TODO: Complete member initialization
			this.source = source;
			this.target = target;
			this.dmg = dmg;
			this.actualDmg = actualDmg;
		}


		public int Damage
		{
			get { return dmg; }
		}


		public int ActualDmg
		{
			get { return actualDmg; }
		}

		public Unit Source
		{
			get { return source; }
		}

		public Unit Target
		{
			get { return target; }
		}
	}
}