using System;
using System.Threading;
using GooseEngine;
using GooseEngine.Entities.Units;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Actions;
using GooseEngine.GameManagement.Events;
using JSLibrary.Data;
using NUnit.Framework;

namespace GooseEngine_Test
{
	[TestFixture]
	public class GameEngineTest
	{
		[Test]
		public void RunningGame_MoveActionStarted_MoveActionCompletes()
		{
			GooseWorld world = new GooseWorld(new GooseMap(new Size(2, 2)));

			ActionManager actman = new ActionManager();
			GooseFactory factory = new GooseFactory(actman);
			EventManager evtman = new EventManager();
			Agent a = new Agent();
			a.ActionManager = actman;

			evtman.AddEntity(a);
			world.AddEntity(new Point(0, 0), a);
			GooseModel engine = new GooseModel(world, actman, evtman, factory);

			Thread thread = new Thread(() => engine.Start());
			thread.Name = "Engine Thread";

			MoveUnit move = new MoveUnit(new Vector(0, 1));

			evtman.Register(new Trigger<UnitMovePostEvent>(_ => actman.Queue(new CloseEngine())));

			thread.Start();

			a.QueueAction(move);

			thread.Join();
			Exception e;
			Assert.IsFalse(engine.EngineCrashed(out e));
		}
	}
}