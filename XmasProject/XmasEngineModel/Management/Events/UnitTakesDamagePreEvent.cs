using System;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel.Management.Events
{
	public class UnitTakesDamagePreEvent : XmasEvent
	{
		private int dmg;
		private double dmgMultiplier = 1;
		private int dmgPostMultiplier;
		private int dmgPreMultiplier;
		private Agent source;
		private Agent target;

		public UnitTakesDamagePreEvent(Agent source, Agent target, int dmg)
		{
			this.source = source;
			this.target = target;
			this.dmg = dmg;
		}

		public int Damage
		{
			get { return dmg; }
		}


		public int ActualDmg
		{
			get
			{
				double pre = dmgPreMultiplier;
				double post = dmgPostMultiplier;
				double dmg = this.dmg;
				double realdmg = ((dmg + pre)*dmgMultiplier) + post;
				return (int) Math.Round(realdmg);
			}
		}

		public void ModDmgPreMultiplier(int p)
		{
			dmgPreMultiplier += p;
		}

		public void ModDmgPostMultiplier(int p)
		{
			dmgPostMultiplier += p;
		}

		public void ModDmgMultiplier(double p)
		{
			dmgMultiplier *= p;
		}
	}
}