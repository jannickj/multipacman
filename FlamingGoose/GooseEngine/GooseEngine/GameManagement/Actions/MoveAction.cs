using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GameEngine.ActionManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;
using GooseEngine.Entities;
using GooseEngine.Interfaces;

namespace GooseEngine.GameManagement.Actions
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

        protected override void Execute(IGameManager gem)
        {
            UnitMovePreEvent before = new UnitMovePreEvent();
            gem.Raise(before);
            GameTimer gt = new GameTimer(() =>
            {
                //move unit with world
                gem.Raise(new UnitMovePostEvent());

                this.Complete();

            });

            if(!before.IsStopped)
                gt.StartSingle(1);
           
        }

    }
}
