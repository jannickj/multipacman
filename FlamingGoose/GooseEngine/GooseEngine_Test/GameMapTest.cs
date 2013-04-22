using System.Linq;
using GooseEngine;
using GooseEngine.Entities;
using JSLibrary.Data;
using NUnit.Framework;
using GooseEngine.Entities.Units;

namespace GooseEngine_Test
{
	[TestFixture]
	public class GameMapTest
	{
		[Test]
		public void OutOfBoundsTile_GetTileOutSideMap_ReturnsTileWithImpassableWall()
		{
			GooseMap map = new GooseMap (new Size (0, 0));
			Entity actual = map [0, 1].Entities.First ();

			Assert.IsInstanceOf<ImpassableWall> (actual);
		}

		[Test]
		public void BuildWallChunk_BuildSquareWall_ReturnsSquareWall()
		{
			GooseMap map = new GooseMap (new Size (1, 1));
			map.AddChunk<Wall> (new Point (-1, -1), new Point (1, 1));

			foreach (Tile tile in map.Tiles)
				Assert.IsInstanceOf<Wall> (tile.Entities.First ());
		}

		[Test]
		public void OutOfBoundsWallChunk_RequestBuildWallOutsideMap_ReturnWallNotBuilt()
		{
			GooseMap map = new GooseMap (new Size (0, 0));
			map.AddChunk<Wall> (new Point (1, 1), new Point (1, 1));
			Entity actual = map [1, 1].Entities.First ();

			Assert.IsInstanceOf<ImpassableWall> (actual);
		}

		[Test] 
		public void MisplacedWallChunk_RequestBuildWallOnUnit_ReturnWallNotBuilt()
		{
			GooseMap map = new GooseMap (new Size (0, 0));

			map [0, 0].AddEntity (new Player ());
			map.AddChunk<Wall> (new Point (0, 0), new Point (0, 0));

			Assert.That (map [0, 0].Entities, Has.Some.InstanceOf<Player> ());
			Assert.That (map [0, 0].Entities, Has.None.InstanceOf<Wall> ());
		}
	}
}