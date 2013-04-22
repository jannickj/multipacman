using System;
using System.Linq;
using GooseEngine;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using NUnit.Framework;
using GooseEngine;


namespace GooseEngine_Test
{
    [TestFixture()]
    public class GameMapTest
    {
        [Test()]
        public void getGrid_AdjacentToOuterBounds_ReturnsImpassableWalls()
        {
			GooseMap map = new GooseMap (new Size (0, 0));
			Tile actual = map [0, 1];

			Assert.IsInstanceOf<ImpassableWall>(actual.Entities.First());
        }
    }
}
