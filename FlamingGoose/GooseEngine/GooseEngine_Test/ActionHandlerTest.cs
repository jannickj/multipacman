using System;
using System.Collections.Generic;
using System.Drawing;
using GooseEngine;
using GooseEngine.Actions;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using GooseEngine.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GooseEngine_Test
{
    [TestClass]
    public class ActionHandlerTest
    {
        GameWorld world;

        [TestInitialize]
        public void Initialize()
        {
            int grid_size = 5;
            Tile[,] ters = new Tile[grid_size, grid_size];
            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    ters[i, j] = new Terrain();
                }
            }
            world = new GameWorld(ters);
        }

        [TestMethod]
        public void Move_AgentInMiddleOfMap_ItMoves()
        {
            ActionHandler actionhandler = new ActionHandler(world);
            Agent agent = new Agent();
            world.AddEntity(new Point(2, 2), agent);
            

            Move m = new Move(agent, Direction.Right, -1);

            bool eventfired = false;
            m.Completed += delegate(object sender, EventArgs e)
            {
                eventfired = true;
            };

            actionhandler.Enqueue(agent, m);

            actionhandler.Execute();

            Assert.IsTrue(eventfired);

            Point expected = new Point(3,2);

            Point actual = world.GetEntityPosition(agent);

            Assert.AreEqual(expected, actual);
            

            
            
        }

    }
}
