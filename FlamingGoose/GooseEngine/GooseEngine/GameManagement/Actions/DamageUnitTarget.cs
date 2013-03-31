using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.ActionManagement;
using GooseEngine.Entities;
using GooseEngine.GameManagement.Events;
using GooseEngine.Interfaces;

namespace GooseEngine.GameManagement.Actions
{
    public class DamageUnitTarget : GameAction
    {
        Unit source;
        Unit target;
        int dmg;

        public DamageUnitTarget(Unit source, Unit target, int dmg)
        {
            this.source = source;
            this.target = target;
            this.dmg = dmg;
        }


        protected override void Execute(IGameManager gem)
        {
            UnitTakesDamagePreEvent pre = new UnitTakesDamagePreEvent(source, target, dmg);
            target.Raise(pre);
            int actualDamage = pre.ActualDmg;
            int newhp = this.target.Health - actualDamage;
            source.Health = newhp < 0? 0 : newhp;
            UnitTakesDamagePostEvent post = new UnitTakesDamagePostEvent(source, target, dmg, actualDamage);
            target.Raise(post);
            this.Complete();
        }

    }
}
