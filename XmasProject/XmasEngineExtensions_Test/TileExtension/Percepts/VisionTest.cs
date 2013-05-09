using System;
using System.Collections.Generic;
using System.Linq;
using JSLibrary.Data;
using NUnit.Framework;
using XmasEngineModel;
using XmasEngineModel.Entities;
using XmasEngineModel.Entities.Units;
using XmasEngineModel.Perceptions;
using XmasEngineExtensions.TileExtension;

namespace XmasEngine_Test.Model.Percepts
{
	[TestFixture]
	public class VisionTest
	{
		[Test]
		public void AgentRequests7by7VisionInEmpty3by3World_Return5by5TilesWithImpassableWallsAtEdges()
		{
			XmasMap map = new XmasMap(new Size(1, 1));

			 TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 3, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, -1),
					new Point(-2, -2),
					new Point(-1, -2),
					new Point(-3, -2),
					new Point(-3, -3),
					new Point(-2, -3)
				};

			expected.RemoveAll(kv => unlistedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			foreach (KeyValuePair<Point, Tile> kv in actual)
			{
				Console.Write(kv.Key);
				if (kv.Value.Entities.Count > 0)
					Console.Write(kv.Value.Entities.First().GetType().Name);
				Console.WriteLine("");
			}

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void AgentWithSecondRingFilledByWalls7By7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing entity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * SSSSSSS
			 * SWWWWWS
			 * SWXXXWS
			 * SWXPXWS
			 * SWXXXWS
			 * SWWWWWS
			 * SSSSSSS
			 */

			XmasMap map = new XmasMap(new Size(3, 3));
			for (int i = -2; i <= 2; i++)
			{
				map[-2, i].AddEntity(new ImpassableWall());
				map[2, i].AddEntity(new ImpassableWall());
			}

			for (int i = -1; i <= 1; i++)
			{
				map[i, -2].AddEntity(new ImpassableWall());
				map[i, 2].AddEntity(new ImpassableWall());
			}

			map[-3, 0].AddEntity(new Wall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 3, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> listedTiles = new List<Point>();

			for (int i = -2; i <= 2; i++)
				for (int j = -2; j <= 2; j++)
					listedTiles.Add(new Point(i, j));
			listedTiles.Remove(new Point(2, 2));
			listedTiles.Remove(new Point(-2, -2));
			listedTiles.Remove(new Point(-2, 2));
			listedTiles.Remove(new Point(2, -2));


			expected.RemoveAll(kv => !listedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			List<KeyValuePair<Point, Tile>> act_diff = actual.Except(expected).ToList();
			List<KeyValuePair<Point, Tile>> exp_diff = expected.Except(actual).ToList();
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void AgentWithTwoOuterRingsFilledWithWalls7By7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing entity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * WWWWWWW
			 * WWWWWWW
			 * WWXXXWW
			 * WWXPXWW
			 * WWXXXWW
			 * WWWWWWW
			 * WWWWWWW
			 */

			XmasMap map = new XmasMap(new Size(3, 3));

			for (int x = -3; x <= 3; x++)
			{
				for (int y = -3; y <= 3; y++)
				{
					if ((x < -1 || x > 1) && (y < -1 || y > 1))
						map[x, y].AddEntity(new Wall());
				}
			}

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 3, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> listedTiles = new List<Point>();

			for (int i = -2; i <= 2; i++)
				for (int j = -2; j <= 2; j++)
					listedTiles.Add(new Point(i, j));
			listedTiles.Remove(new Point(2, 2));
			listedTiles.Remove(new Point(-2, -2));
			listedTiles.Remove(new Point(-2, 2));
			listedTiles.Remove(new Point(2, -2));


			expected.RemoveAll(kv => !listedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			List<KeyValuePair<Point, Tile>> act_diff = actual.Except(expected).ToList();
			List<KeyValuePair<Point, Tile>> exp_diff = expected.Except(actual).ToList();
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void AgentWithWallToTheWest7by7Grid_CorrectVisionObject()
		{
			/* This is our expected vision. Legend: 
			 * (P): The viewing entity
			 * (W): A vision blocking wall
			 * (X): Visible tiles
			 * (S): Invisible tiles
			 * 
			 * XXXXXXX
			 * XXXXXXX
			 * XXXXXXX
			 * SSWPXXX
			 * XXXXXXX
			 * XXXXXXX
			 * XXXXXXX
			 */

			XmasMap map = new XmasMap(new Size(3, 3));
			map[-1, 0].AddEntity(new Wall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, 0),
					new Point(-3, 0)
				};

			expected.RemoveAll(kv => unlistedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			List<KeyValuePair<Point, Tile>> act_diff = actual.Except(expected).ToList();
			List<KeyValuePair<Point, Tile>> exp_diff = expected.Except(actual).ToList();
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void AgentWithWallsInCornersFiveTimesFiveGrid_CorrectVisionObject()
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

			XmasMap map = new XmasMap(new Size(2, 2));
			map[-1, -1].AddEntity(new Wall());
			map[1, -1].AddEntity(new Wall());
			map[-1, 1].AddEntity(new Wall());
			map[1, 1].AddEntity(new Wall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, -2),
					new Point(-1, -2),
					new Point(-2, -1),
					new Point(1, -2),
					new Point(2, -2),
					new Point(2, -1),
					new Point(-2, 1),
					new Point(-2, 2),
					new Point(-1, 2),
					new Point(1, 2),
					new Point(2, 2),
					new Point(2, 1)
				};

			expected.RemoveAll(kv => unlistedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void AgentWithWallsInNWCorner7by7Grid_CorrectVisionObject()
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

			XmasMap map = new XmasMap(new Size(3, 3));
			map[-1, -1].AddEntity(new Wall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, -1),
					new Point(-2, -2),
					new Point(-1, -2),
					new Point(-3, -2),
					new Point(-3, -3),
					new Point(-2, -3)
				};

			expected.RemoveAll(kv => unlistedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			Assert.That(expected, Is.EquivalentTo(actual));
		}

		[Test]
		public void AgentWithWallsOnSidesFiveTimesFiveGrid_CorrectVisionObject()
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

			XmasMap map = new XmasMap(new Size(2, 2));
			map[-1, 0].AddEntity(new Wall());
			map[1, 0].AddEntity(new Wall());
			map[0, -1].AddEntity(new Wall());
			map[0, 1].AddEntity(new Wall());

			TileWorld world = new TileWorld(map);
			Vision vision = world.View(new Point(0, 0), 2, new Player());

			List<KeyValuePair<Point, Tile>> expected = vision.AllTiles();
			List<Point> unlistedTiles = new List<Point>
				{
					new Point(-2, 0),
					new Point(2, 0),
					new Point(0, -2),
					new Point(0, 2)
				};

			expected.RemoveAll(kv => unlistedTiles.Contains(kv.Key));
			List<KeyValuePair<Point, Tile>> actual = (List<KeyValuePair<Point, Tile>>) vision.VisibleTiles;
			Assert.AreEqual(expected, actual);
		}
	}
}