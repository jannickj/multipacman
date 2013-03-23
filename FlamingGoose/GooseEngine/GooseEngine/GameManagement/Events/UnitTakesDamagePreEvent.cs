using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Entities;

namespace GooseEngine.GameManagement.Events
{
    public class UnitTakesDamagePreEvent : GameEvent
    {
        private Unit source;
        private Unit target;
        private int dmg;
        double dmgMultiplier = 1;
        int dmgPreMultiplier = 0;
        int dmgPostMultiplier = 0;

        public UnitTakesDamagePreEvent(Unit source, Unit target, int dmg)
        {
            // TODO: Complete member initialization
            this.source = source;
            this.target = target;
            this.dmg = dmg;
        }
        public void ModDmgPreMultiplier(int p)
        {
            this.dmgPreMultiplier += p;
        }

        public void ModDmgPostMultiplier(int p)
        {
            this.dmgPostMultiplier += p;
        }

        public void ModDmgMultiplier(double p)
        {
            this.dmgMultiplier *= p;
        }

        public int Damage
        {
            get { return dmg; }
        }


        public int ActualDmg
        {
            get 
            {
                double pre = this.dmgPreMultiplier;
                double post = this.dmgPostMultiplier;
                double dmg = this.dmg;
                double realdmg = ((dmg+pre) * dmgMultiplier) + post;
                return (int)Math.Round(realdmg); 
            }
        }

    }
}
