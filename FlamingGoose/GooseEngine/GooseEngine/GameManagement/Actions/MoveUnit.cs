using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GooseEngine.GameManagement;
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

        protected override void Execute()
        {
            UnitMovePreEvent before = new UnitMovePreEvent();
            unit.Raise(before);
            GameTimer gt = this.Factory.CreateTimer(() =>
            {
                Point newloc = World.GetEntityPosition(unit) + direction;
                World.SetEntityLocation(newloc, unit);
                unit.Raise(new UnitMovePostEvent());

                this.Complete();

            });

            if(!before.IsStopped)
                gt.StartSingle(time);
           
        }

    }
}
