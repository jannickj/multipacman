using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Events
{
	public class UnitTakesDamagePostEvent : XmasEvent
	{
		private int actualDmg;
		private int dmg;
		private Agent source;
		private Agent target;

		public UnitTakesDamagePostEvent(Agent source, Agent target, int dmg, int actualDmg)
		{
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

		public Agent Source
		{
			get { return source; }
		}

		public Agent Target
		{
			get { return target; }
		}
	}
}