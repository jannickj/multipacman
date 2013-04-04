using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using GooseEngine;
using GooseEngine.Entities.Interactables;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Entities.Units;
using GooseEngine.Percepts;
using NUnit.Framework;
using GooseEngine.Data;


namespace GooseEngine_Test.Percepts
{
    [TestFixture]
    public class VisionTest
    {


        //public void ConstructVision_ 
        

        [Test]
        public void WriteXml_visionWithAgentAndPowerInSight_ThoseTwoWithRelativeCoordsAndTheSurroundingEmptyTiles()
        {
			GameMap map = new GameMap (new Size (2, 2));
			GameWorld world = new GameWorld (map);
			map[-1,-1].AddEntity (new Wall ());
			Vision vision = world.View (new Point (0, 0), 2, new Player ());

			bool blocking = map [-1, -1].IsVisionBlocking (new Player ());
			
			var tiles = vision.VisibleTiles;
			Assert.Fail ();
//            GameMap map = new GameMap(new Size(2, 2));
//            Agent a = new Agent();
//            PowerUp p = new PowerUp();
//            
//            GameWorld world = new GameWorld(map);
//            world.AddEntity(new Point(1, 2), a);
//            world.AddEntity(new Point(1, 2), p);
//
//            Vision v = world.View(new Point(1, 1), 1);
//            // Tést probably falis because xml file is malformed
//            XDocument expected = XDocument.Parse(File.ReadAllText("xmltempl.xml"));
//            
//            StringWriter writer = new StringWriter();
//            XmlWriter xmlwriter = XmlWriter.Create(writer);
//            v.WriteXml(xmlwriter);
//
//            XDocument actual = XDocument.Parse(writer.ToString());
//
//            Assert.IsTrue(XDocument.DeepEquals(expected, actual));
            

        }

    }
}
