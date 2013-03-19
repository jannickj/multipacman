using System;
using System.Drawing;
using System.Linq;
using GooseEngine;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using NUnit.Framework;


namespace GooseEngine_Test
{
    [TestFixture]
    public class GameMapTest
    {

       
        [Test]
        public void getGrid_AdjacentToOuterBounds_ReturnsImpassableWalls()
        {

            GameMap map = new GameMap(new Size(2,2));

            Grid<Tile> g = map[-2, -2, 1];


            object actual = g[0, 0].Entities.First();

            Assert.IsInstanceOf<ImpassableWall>(actual);
            
            
        }
    }
}
