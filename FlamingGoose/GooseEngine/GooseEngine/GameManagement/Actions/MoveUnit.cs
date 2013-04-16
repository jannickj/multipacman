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
    public class MoveUnit : EntityGameAction<Unit>
    {
		private Vector direction;
		private double time;

		///<summary>
		///Initializes a move action, which is used to move entities in a gameworld</summary>
		///<param name="world"> The world the unit is moved in</param>
		///<param name="unit"> The unit that gets moved</param>
		///<param name="direction"> the direction vector of the move</param>
		///<param name="time"> the time in miliseconds that the move takes</param>

//		public MoveUnit(Vector direction, double time)
//        {
//            this.direction = direction.Direction;
//            this.time = time;
//        }

		public MoveUnit(Vector direction)
		{
			this.direction = direction.Direction;
		}

        protected override void Execute()
        {
            UnitMovePreEvent before = new UnitMovePreEvent();
            this.Source.Raise(before);
			time = Source.MoveSpeed;
            GameTimer gt = this.Factory.CreateTimer(() =>
            {
                Point newloc = World.GetEntityPosition(this.Source) + direction;
                World.SetEntityLocation(newloc, this.Source);
                this.Source.Raise(new UnitMovePostEvent());

                this.Complete();
            });

            if(!before.IsStopped)
                gt.StartSingle(time);
           
        }

    }
}
