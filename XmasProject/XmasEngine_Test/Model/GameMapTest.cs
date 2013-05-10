using System.Linq;
using JSLibrary.Data;
using NUnit.Framework;
using XmasEngineModel;
using XmasEngineModel.Entities;
using XmasEngineModel.Entities.Units;

namespace XmasEngine_Test.Model
{
	[TestFixture]
	public class GameMapTest
	{
		[Test]
		public void OutOfBoundsTile_GetTileOutSideMap_ReturnsTileWithImpassableWall()
		{
			TileMap map = new TileMap (new Size (0, 0));
			Entity actual = map [0, 1].Entities.First ();

			Assert.IsInstanceOf<ImpassableWall> (actual);
		}

		[Test]
		public void BuildWallChunk_BuildSquareWall_ReturnsSquareWall()
		{
			TileMap map = new TileMap (new Size (1, 1));
			//map.AddChunk<Wall> (new Point (-1, -1), new Point (1, 1));

			foreach (Tile tile in map.Tiles)
				Assert.IsInstanceOf<Wall> (tile.Entities.First ());
		}

		[Test]
		public void OutOfBoundsWallChunk_RequestBuildWallOutsideMap_ReturnWallNotBuilt()
		{
			TileMap map = new TileMap (new Size (0, 0));
			//map.AddChunk<Wall> (new Point (1, 1), new Point (1, 1));
			Entity actual = map [1, 1].Entities.First ();

			Assert.IsInstanceOf<ImpassableWall> (actual);
		}

		[Test] 
		public void MisplacedWallChunk_RequestBuildWallOnUnit_ReturnWallNotBuilt()
		{
			TileMap map = new TileMap (new Size (0, 0));

			map [0, 0].AddEntity (new Player ());
			//map.AddChunk<Wall> (new Point (0, 0), new Point (0, 0));

			Assert.That (map [0, 0].Entities, Has.Some.InstanceOf<Player> ());
			Assert.That (map [0, 0].Entities, Has.None.InstanceOf<Wall> ());
		}
	}
}