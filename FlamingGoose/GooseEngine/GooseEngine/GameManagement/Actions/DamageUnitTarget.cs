using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.GameManagement;
using GooseEngine.Entities;
using GooseEngine.GameManagement.Events;

namespace GooseEngine.GameManagement.Actions
{
    public class DamageUnitTarget : EntityGameAction<Unit>
    {
        Unit target;
        int dmg;

        public DamageUnitTarget(Unit target, int dmg)
        {
            this.target = target;
            this.dmg = dmg;
        }


        protected override void Execute()
        {
            UnitTakesDamagePreEvent pre = new UnitTakesDamagePreEvent(Source, target, dmg);
            target.Raise(pre);
            int actualDamage = pre.ActualDmg;
            int newhp = this.target.Health - actualDamage;
            Source.Health = newhp < 0? 0 : newhp;
            UnitTakesDamagePostEvent post = new UnitTakesDamagePostEvent(Source, target, dmg, actualDamage);
            target.Raise(post);
            this.Complete();
        }

    }
}
