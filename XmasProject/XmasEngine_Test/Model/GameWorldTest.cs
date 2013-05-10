using JSLibrary.Data;
using NUnit.Framework;
using XmasEngineExtensions.TileExtension;

namespace XmasEngine_Test.Model
{
	[TestFixture]
	public class GameWorldTest
	{
		[Test]
		public void GetEntityPosition_OneAgentInWorld_ReturnThatAgentPosition()
		{
			TileMap map = new TileMap(new Size(2, 2));
			TileWorld world = new TileWorld(map);

			Agent agent = new Agent();
			world.AddEntity(new Point(1, 2), agent);

			Point expected = new Point(1, 2);
			Point actual = ((TilePosition) world.GetEntityPosition(agent)).Point;
			Assert.AreEqual(expected, actual);
		}
	}
}