using System;
using System.Threading;
using GooseEngine.ActionManagement;
using GooseEngine;
using GooseEngine.Entities.Units;
using NUnit.Framework;
using GooseEngine.GameManagement.Actions;
using System.Collections.Generic;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;
using GooseEngine.Data;

namespace GooseEngine_Test
{
    [TestFixture]
    public class GameEngineTest
    {
        
        [Test]
        public void runningGame_MoveActionStarted_MoveActionCompletes()
        {
            GameWorld world = new GameWorld(new GameMap(new Size(2, 2)));

            GameManager manager = new GameManager(world);
            Agent a = new Agent();
            world.AddEntity(new Point(0, 0), a);
            manager.AddEntity(a);
            GameEngine engine = new GameEngine(manager);

            Thread thread = new Thread(new ThreadStart(() => engine.Start()));

            MoveUnit move = new MoveUnit(a, new Vector(0,1), 0.01);

            manager.Register(new Trigger<UnitMovePostEvent>(_ => manager.Queue(new CloseEngine())));

            manager.Queue(move);

            thread.Start();

            Assert.True(thread.Join(500));





        }
    }
}
