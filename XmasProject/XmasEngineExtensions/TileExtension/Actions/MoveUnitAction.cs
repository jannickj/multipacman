﻿using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineExtensions.TileExtension.Modules;

namespace XmasEngineExtensions.TileExtension.Actions
{
	public class MoveUnitAction : EntityXmasAction<Agent>
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
//		public MoveUnitAction(Vector direction, double time)
//        {
//            this.direction = direction.Direction;
//            this.time = time;
//        }

		public MoveUnitAction(Vector direction)
		{
			this.direction = direction.Direction;
		}

		protected override void Execute()
		{
			UnitMovePreEvent before = new UnitMovePreEvent();
			Source.Raise(before);
			time = Source.Module<SpeedModule>().Speed;
			XmasTimer gt = Factory.CreateTimer(this,() =>
				{
					TilePosition tile = World.GetEntityPosition(Source) as TilePosition;
					Point newloc = tile.Point + direction;
					World.SetEntityPosition(Source, new TilePosition(newloc));
					Source.Raise(new UnitMovePostEvent(newloc));

					Complete();
				});

			if (!before.IsStopped)
				gt.StartSingle(time);
		}
	}
}