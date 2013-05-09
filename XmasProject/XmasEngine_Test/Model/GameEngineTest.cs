using System;
using System.Threading;
using JSLibrary.Data;
using NUnit.Framework;
using XmasEngineModel;
using XmasEngineModel.Entities.Units;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;
using XmasEngineModel.Management.Events;

namespace XmasEngine_Test.Model
{
	[TestFixture]
	public class GameEngineTest
	{
		[Test]
		public void RunningGame_MoveActionStarted_MoveActionCompletes()
		{
			TileWorld world = new TileWorld(new XmasMap(new Size(2, 2)));

			ActionManager actman = new ActionManager();
			XmasFactory factory = new XmasFactory(actman);
			EventManager evtman = new EventManager();
			Agent a = new Agent();
			a.ActionManager = actman;

			evtman.AddEntity(a);
			world.AddEntity(new Point(0, 0), a);
			XmasModel engine = new XmasModel(world, actman, evtman, factory);

			Thread thread = new Thread(() => engine.Start());
			thread.Name = "Engine Thread";

			MoveUnitAction move = new MoveUnitAction(new Vector(0, 1));

			evtman.Register(new Trigger<UnitMovePostEvent>(_ => actman.Queue(new CloseEngineAction())));

			thread.Start();

			a.QueueAction(move);

			thread.Join();
			Exception e;
			Assert.IsFalse(engine.EngineCrashed(out e));
		}
	}
}