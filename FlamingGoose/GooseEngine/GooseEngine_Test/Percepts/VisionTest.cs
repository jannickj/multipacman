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
using System.Collections.Generic;


namespace GooseEngine_Test.Percepts
{
    [TestFixture]
    public class VisionTest
    {
        [Test]
		public void AgentWithWallsInCornersFiveTimesFiveGrid_CorrectVisionObject ()
        {
			/* This is our expected vision. Legend: 
			 * (P): The viewing entity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * SSXSS
			 * SWXWS
			 * XXPXX
			 * SWXWS
			 * SSXSS
			 */

			GameMap map = new GameMap (new Size (2, 2));
			map [-1, -1].AddEntity (new Wall ());
			map [1, -1].AddEntity (new Wall ());
			map [-1, 1].AddEntity (new Wall ());
			map [1, 1].AddEntity (new Wall ());

			GameWorld world = new GameWorld (map);
			Vision vision = world.View (new Point (0, 0), 2, new Player ());

			List<KeyValuePair<Point,Tile>> expected = vision.AllTiles ();
			List<Point> unlistedTiles = new List<Point> ()
			{
				new Point(-2,-2), new Point(-1,-2), new Point(-2,-1),
				new Point(1,-2), new Point(2,-2), new Point(2,-1),
				new Point(-2,1), new Point(-2,2), new Point(-1,2),
				new Point(1,2), new Point(2,2), new Point(2,1)
			};

			expected.RemoveAll (kv => unlistedTiles.Contains (kv.Key)); 
			List<KeyValuePair<Point,Tile>> actual = (List<KeyValuePair<Point,Tile>>)vision.VisibleTiles;
			Assert.AreEqual (expected, actual);
        }

		[Test]
		public void AgentWithWallsOnSidesFiveTimesFiveGrid_CorrectVisionObject ()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing entity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * XXSXX
			 * XXWXX
			 * SWPWS
			 * XXWXX
			 * XXSXX
			 */
			
			GameMap map = new GameMap (new Size (2, 2));
			map [-1, 0].AddEntity (new Wall ());
			map [1, 0].AddEntity (new Wall ());
			map [0, -1].AddEntity (new Wall ());
			map [0, 1].AddEntity (new Wall ());
			
			GameWorld world = new GameWorld (map);
			Vision vision = world.View (new Point (0, 0), 2, new Player ());
			
			List<KeyValuePair<Point,Tile>> expected = vision.AllTiles ();
			List<Point> unlistedTiles = new List<Point> ()
			{
				new Point(-2,0), new Point(2,0), new Point(0,-2), new Point(0,2)
			};
			
			expected.RemoveAll (kv => unlistedTiles.Contains (kv.Key)); 
			List<KeyValuePair<Point,Tile>> actual = (List<KeyValuePair<Point,Tile>>)vision.VisibleTiles;
			Assert.AreEqual (expected, actual);
		}

		[Test]
		public void AgentWithWallsInNWCornerSixTimesSixGrid_CorrectVisionObject ()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing entity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * SSXXXXX
			 * SSSXXXX
			 * XSWXXXX
			 * XXXPXXX
			 * XXXXXXX
			 * XXXXXXX
			 * XXXXXXX
			 */
			
			GameMap map = new GameMap (new Size (2, 2));
			map [-1, -1].AddEntity (new Wall ());
			
			GameWorld world = new GameWorld (map);
			Vision vision = world.View (new Point (0, 0), 2, new Player ());
			
			List<KeyValuePair<Point,Tile>> expected = vision.AllTiles ();
			List<Point> unlistedTiles = new List<Point> ()
			{
				new Point(-2,-1), new Point(-2,-2), new Point(-1,-2),
				new Point(-3,-2), new Point(-3,-3), new Point(-2,-3)
			};
			
			expected.RemoveAll (kv => unlistedTiles.Contains (kv.Key)); 
			List<KeyValuePair<Point,Tile>> actual = (List<KeyValuePair<Point,Tile>>)vision.VisibleTiles;
			Assert.AreEqual (expected, actual);
		}
    }
}
