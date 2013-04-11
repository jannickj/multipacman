using System;
using System.Linq;
using GooseEngine.Entities.Interactables;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using NUnit.Framework;
using GooseEngine;


namespace GooseEngine_Test.Entities.MapEntities
{
    [TestFixture]
    public class TileTest
    {
        [Test]
        public void GetEntities_tileWithAnAgent_ReturnThatAgent()
        {
            Agent a = new Agent();
            Tile t = new Tile();
            t.AddEntity(a);

            Agent expected = a;
            Agent actual = t.Entities.OfType<Agent>().First();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanContain_aWall_ReturnsFalse()
        {
            Agent a = new Agent();
            Tile t = new Tile();
            Wall wall = new Wall();
            t.AddEntity(wall);
            Assert.IsFalse(t.CanContain(a));
        }

        [Test]
        public void CanContain_emptyTerrain_Returnstrue()
        {
            Agent a = new Agent();
            Tile t = new Tile();
            Assert.IsTrue(t.CanContain(a));
        }

        [Test]
        public void CanContain_TerrainWithPowerUp_Returnstrue()
        {
            Agent a = new Agent();
            PowerUp p = new PowerUp();
            PowerUp p2 = new PowerUp();
            Tile t = new Tile();
            t.AddEntity(p);
            Assert.IsTrue(t.CanContain(a));
            Assert.IsTrue(t.CanContain(p2));
        }

        [Test]
        public void CanContain_terrainWithAnAgent_Returnsfalse()
        {
            Agent a = new Agent();
            Agent b = new Agent();
            PowerUp p = new PowerUp();
            Tile t = new Tile();
            t.AddEntity(a);
            Assert.IsFalse(t.CanContain(b));
            Assert.IsTrue(t.CanContain(p));
        }
    }
}
