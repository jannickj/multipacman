using System;
using System.Linq;
using GooseEngine.Entities.Interactables;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GooseEngine_Test.Entities.MapEntities
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void GetEntities_tileWithAnAgent_ReturnThatAgent()
        {
            Agent a = new Agent();
            Terrain t = new Terrain();
            t.AddEntity(a);

            Agent expected = a;
            Agent actual = t.Entities.OfType<Agent>().First();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanContain_aWall_ReturnsFalse()
        {
            Agent a = new Agent();
            Wall wall = new Wall();
            Assert.IsFalse(wall.CanContain(a));
        }

        [TestMethod]
        public void CanContain_emptyTerrain_Returnstrue()
        {
            Agent a = new Agent();
            Terrain t = new Terrain();
            Assert.IsTrue(t.CanContain(a));
        }

        [TestMethod]
        public void CanContain_TerrainWithPowerUp_Returnstrue()
        {
            Agent a = new Agent();
            PowerUp p = new PowerUp();
            PowerUp p2 = new PowerUp();
            Terrain t = new Terrain();
            t.AddEntity(p);
            Assert.IsTrue(t.CanContain(a));
            Assert.IsTrue(t.CanContain(p2));
        }

        [TestMethod]
        public void CanContain_terrainWithAnAgent_Returnsfalse()
        {
            Agent a = new Agent();
            Agent b = new Agent();
            PowerUp p = new PowerUp();
            Terrain t = new Terrain();
            t.AddEntity(a);
            Assert.IsFalse(t.CanContain(b));
            Assert.IsTrue(t.CanContain(p));
        }
    }
}
