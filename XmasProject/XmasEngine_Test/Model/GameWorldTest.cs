using JSLibrary.Data;
using NUnit.Framework;
using XmasEngineModel;
using XmasEngineModel.Entities.Units;

namespace XmasEngine_Test.Model
{
	[TestFixture]
	public class GameWorldTest
	{
		[Test]
		public void GetEntityPosition_OneAgentInWorld_ReturnThatAgentPosition()
		{
			XmasMap map = new XmasMap(new Size(2, 2));
			TileWorld world = new TileWorld(map);

			Agent agent = new Agent();
			world.AddEntity(new Point(1, 2), agent);

			Point expected = new Point(1, 2);
			Point actual = world.GetEntityPosition(agent);
			Assert.AreEqual(expected, actual);
		}
	}
}