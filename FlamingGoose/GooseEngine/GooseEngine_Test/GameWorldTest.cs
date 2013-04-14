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
