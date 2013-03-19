using System;
using System.Drawing;
using System.Linq;
using GooseEngine;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GooseEngine_Test
{
    [TestClass]
    public class GameMapTest
    {

       
        [TestMethod]
        public void getGrid_AdjacentToOuterBounds_ReturnsImpassableWalls()
        {

            GameMap map = new GameMap(new Size(2,2));

            Grid<Tile> g = map[-2, -2, 1];


            object actual = g[0, 0].Entities.First();
            Type expected = typeof(ImpassableWall);

            Assert.IsInstanceOfType(actual, expected);
            
            
            
        }
    }
}
