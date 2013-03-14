using System;
using System.Drawing;
using GooseEngine;
using GooseEngine.Entities.Interactables;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using GooseEngine.Percepts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GooseEngine_Test.Percepts
{
    [TestClass]
    public class VisionTest
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
        public void TestMethod1()
        {

            Agent a = new Agent();
            PowerUp p = new PowerUp();
            
            GameWorld world = new GameWorld(ters);
            world.AddEntity(new Point(1, 2), a);
            world.AddEntity(new Point(1, 2), p);

            Vision v = world.View(new Point(1, 1), 1);

        }
    }
}
