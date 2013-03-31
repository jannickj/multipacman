using System;
using System.Drawing;
using System.Threading;
using GooseEngine.ActionManagement;
using GooseEngine;
using GooseEngine.Entities.Units;
using NUnit.Framework;
using GooseEngine.GameManagement.Actions;
using System.Collections.Generic;

namespace GooseEngine_Test
{
    [TestFixture]
    public class GameEngineTest
    {
        
        [Test]
        public void runningGame_TimedAction_TimedActionGets()
        {
            GameWorld world = new GameWorld(new GameMap(new Size(2, 2)));

            GameManager manager = new GameManager(world);
            Agent a = new Agent();
            world.AddEntity(new Point(0, 0), a);
            manager.AddEntity(a);
            GameEngine engine = new GameEngine(manager);

            Thread thread = new Thread(new ThreadStart(() => engine.Start()));

            MoveUnit move = new MoveUnit(a, new GooseEngine.Data.Vector(0,1), 0.01);

            manager.Queue(move);





        }
    }
}
