using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GooseEngine.ActionManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;
using GooseEngine.Entities;

namespace GooseEngine.GameManagement.Actions
{
    public class MoveUnit : GameAction
    {
        Unit unit;
        Vector direction;
        double time;

         ///<summary>
         ///Initializes a move action, which is used to move entities in a gameworld</summary>
         ///<param name="world"> The world the unit is moved in</param>
         ///<param name="unit"> The unit that gets moved</param>
         ///<param name="direction"> the direction vector of the move</param>
         ///<param name="time"> the time in miliseconds that the move takes</param>
        public MoveUnit(Unit unit, Vector direction, double time)
        {
            this.unit = unit;
            this.direction = direction.Direction;
            this.time = time;
        }

        protected override void Execute(EventManager gem)
        {
            UnitMovePreEvent before = new UnitMovePreEvent();
            gem.Raise(before);
            GameTimer gt = gem.CreateTimer(() =>
            {
                Point newloc = gem.World.GetEntityPosition(unit) + direction;
                gem.World.SetEntityLocation(newloc, unit);
                gem.Raise(new UnitMovePostEvent());

                this.Complete();

            });

            if(!before.IsStopped)
                gt.StartSingle(1);
           
        }

    }
}
