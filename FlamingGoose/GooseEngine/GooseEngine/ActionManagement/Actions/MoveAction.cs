using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GameEngine.ActionManagement;
using GooseEngine.ActionManagement.Events;
using GooseEngine.Data;
using GooseEngine.Entities;

namespace GooseEngine.ActionManagement.Actions
{
    public class MoveAction : GameAction
    {
         ///<summary>
         ///Initializes a move action, which is used to move entities in a gameworld</summary>
         ///<param name="world"> The world the unit is moved in</param>
         ///<param name="unit"> The unit that gets moved</param>
         ///<param name="directio"> the direction vector of the move</param>
         ///<param name="time"> the time in miliseconds that the move takes</param>
        public MoveAction(GameWorld world, Unit unit, Vector direction, double time)
        {

        }

        protected override void Execute(GameEventManager gem)
        {
            UnitIsMovingEvent before = new UnitIsMovingEvent();
            gem.Raise(before);
            GameTimer gt = new GameTimer(() =>
            {
                //move unit with world
                gem.Raise(new UnitHasMovedEvent());
            });

            if(!before.IsInterrupted)
                gt.StartSingle(1);
           
        }
    }
}
