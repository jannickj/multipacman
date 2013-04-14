using System;
using System.Collections;
using System.Linq;
using GooseEngine;
using GooseEngine.Descriptions;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Percepts;
using GooseEngine.Entities.Units;
using NUnit.Framework;
using GooseEngine.Data;

namespace GooseEngine_Test
{
    [TestFixture]
    public class GameWorldTest
    {
        

        [Test]
        public void GetVisibleTiles_WallBlockingNE_ReturnNoVisionNE()
        {

            GameMap map = new GameMap(new Size(2, 2));
            Wall SEwall = new Wall();
            Wall NearWall = new Wall();
           
            GameWorld gm = new GameWorld(map);

            gm.AddEntity(new Point(1, 1), NearWall);
            gm.AddEntity(new Point(2, 2), SEwall);

            Vision v = gm.View(new Point(0,0),2, new Player());

           
            int expected_count = 0;
            int actual_count = v.VisibleTiles.Where(p => p.Key.Equals(new Point(2, 2))).Count();
            Assert.AreEqual(expected_count, actual_count);

            Entity expected = NearWall;
            Entity actual = v.VisibleTiles.Where(p => p.Key.Equals(new Point(1, 1))).Select(p => p.Value.Entities.First()).First();
            Assert.AreEqual(expected, actual);


        }

        [Test]
        public void GetEntityPosition_OneAgentInWorld_ReturnThatAgentPosition()
        {
            GameMap map = new GameMap(new Size(2, 2));
            GameWorld world = new GameWorld(map);

            Agent agent = new Agent();
            world.AddEntity(new Point(1, 2), agent);

            Point expected = new Point(1, 2);
            Point actual = world.GetEntityPosition(agent);
            Assert.AreEqual(expected, actual);
        }
    }
}
