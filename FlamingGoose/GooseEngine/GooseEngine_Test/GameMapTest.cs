using System.Linq;
using GooseEngine;
using GooseEngine.Entities;
using JSLibrary.Data;
using NUnit.Framework;

namespace GooseEngine_Test
{
	[TestFixture]
	public class GameMapTest
	{
		[Test]
		public void getGrid_AdjacentToOuterBounds_ReturnsImpassableWalls()
		{
			GooseMap map = new GooseMap(new Size(2, 2));

			Grid<Tile> g = map[-2, -2, 1];


			object actual = g[0, 0].Entities.First();

			Assert.IsInstanceOf<ImpassableWall>(actual);
		}
	}
}