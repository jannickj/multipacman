using GooseEngine;
using GooseEngine.Entities.Units;
using JSLibrary.Data;
using NUnit.Framework;

namespace GooseEngine_Test
{
	[TestFixture]
	public class GameWorldTest
	{
		[Test]
		public void GetEntityPosition_OneAgentInWorld_ReturnThatAgentPosition()
		{
			GooseMap map = new GooseMap(new Size(2, 2));
			GooseWorld world = new GooseWorld(map);

			Agent agent = new Agent();
			world.AddEntity(new Point(1, 2), agent);

			Point expected = new Point(1, 2);
			Point actual = world.GetEntityPosition(agent);
			Assert.AreEqual(expected, actual);
		}
	}
}