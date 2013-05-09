using JSLibrary.Data;
using XmasEngineModel.Entities;
using XmasEngineModel.GameManagement.Events;

namespace XmasEngineModel.GameManagement.Actions
{
	public class MoveUnit : EntityGameAction<Unit>
	{
		private Vector direction;
		private double time;

		/// <summary>
		///     Initializes a move action, which is used to move entities in a gameworld
		/// </summary>
		/// <param name="world"> The world the unit is moved in</param>
		/// <param name="unit"> The unit that gets moved</param>
		/// <param name="direction"> the direction vector of the move</param>
		/// <param name="time"> the time in miliseconds that the move takes</param>
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
			Source.Raise(before);
			time = Source.MoveSpeed;
			GameTimer gt = Factory.CreateTimer(() =>
				{
					Point newloc = World.GetEntityPosition(Source) + direction;
					World.SetEntityLocation(newloc, Source);
					Source.Raise(new UnitMovePostEvent(newloc));

					Complete();
				});

			if (!before.IsStopped)
				gt.StartSingle(time);
		}
	}
}