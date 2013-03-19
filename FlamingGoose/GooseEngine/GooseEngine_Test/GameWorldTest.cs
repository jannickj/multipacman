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
        

        [TestMethod]
        public void GetVisibleTiles_WallBlockingNW_ReturnNoVisionNW()
        {

            GameMap map = new GameMap(new Size(2, 2));
            Wall SEwall = new Wall();
           
            map[1, 1].AddEntity(new Wall());
            map[4, 4].AddEntity(SEwall);

            GameWorld gm = new GameWorld(map);

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
            GameMap map = new GameMap(new Size(2, 2));
            GameWorld world = new GameWorld(map);

            Agent agent = new Agent();
            world.AddEntity(new Point(1, 4), agent);

            Point expected = new Point(1, 4);
            Point actual = world.GetEntityPosition(agent);
            Assert.AreEqual(expected, actual);
        }
    }
}
