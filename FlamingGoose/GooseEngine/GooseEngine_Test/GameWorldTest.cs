using System;
using System.Drawing;
using System.Collections;
using System.Linq;
using GooseEngine;
using GooseEngine.Descriptions;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Percepts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GooseEngine.Entities.Units;

namespace GooseEngine_Test
{
    [TestClass]
    public class GameWorldTest
    {
        Tile[,] ters;

        [TestInitialize]
        public void Initialize()
        {
            int grid_size = 5;
            ters = new Tile[grid_size, grid_size];
            for (int i = 0; i < grid_size; i++)
            {
                for (int j = 0; j < grid_size; j++)
                {
                    ters[i, j] = new Terrain();
                }
            }
        }

        [TestMethod]
        public void GetVisibleTiles_WallBlockingNW_ReturnNoVisionNW()
        {
            
            Wall SEwall = new Wall();
           
            ters[1, 1] = new Wall();
            ters[4, 4] = SEwall;
            
            GameWorld gm = new GameWorld(ters);

            Vision v = gm.View(new Point(2,2),2);

           
            int expected_count = 0;
            int actual_count = v.Entities.Where(p => p.Key.Equals(new Point(1, 1))).Count();
            Assert.AreEqual(expected_count, actual_count);

            Entity expected = SEwall;
            Entity actual = v.Entities.Where(p => p.Key.Equals(new Point(4, 4))).Select(p => p.Value).First();
            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void GetEntityPosition_OneAgentInWorld_ReturnThatAgentPosition()
        {

            GameWorld world = new GameWorld(ters);

            Agent agent = new Agent();
            world.AddEntity(new Point(1, 4), agent);

            Point expected = new Point(1, 4);
            Point actual = world.GetEntityPosition(agent);
            Assert.AreEqual(expected, actual);
        }
    }
}
