using System;
using System.Threading;
using GooseEngine.GameManagement;
using GooseEngine;
using GooseEngine.Entities.Units;
using NUnit.Framework;
using GooseEngine.GameManagement.Actions;
using System.Collections.Generic;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;

namespace GooseEngine_Test
{
    [TestFixture]
    public class GameEngineTest
    {
        
        [Test]
        public void RunningGame_MoveActionStarted_MoveActionCompletes()
        {
            GameWorld world = new GameWorld(new GameMap(new Size(2, 2)));
            
            ActionManager actman = new ActionManager();
            GameFactory factory = new GameFactory(actman);
            EventManager evtman = new EventManager();
            Agent a = new Agent();
            a.ActionManager = actman;

            evtman.AddEntity(a);
            world.AddEntity(new Point(0, 0), a);
            GooseModel engine = new GooseModel(world, actman, evtman, factory);

            Thread thread = new Thread(new ThreadStart(() => engine.Start()));
            thread.Name = "Engine Thread";

            MoveUnit move = new MoveUnit(new Vector(0,1));

            evtman.Register(new Trigger<UnitMovePostEvent>(_ => actman.Queue(new CloseEngine())));

            thread.Start();

            a.QueueAction(move);

            thread.Join();
            Exception e;
            Assert.IsFalse(engine.EngineCrashed(out e));





        }
    }
}
