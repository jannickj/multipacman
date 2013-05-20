using XmasEngineExtensions.TileExtension.Modules;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model
{
	public class Player : ConsoleAgent
	{
		public Player() : base("player")
		{
			
		}


		protected override SpeedModule ConstructSpeedModule()
		{
			return new SpeedModule(200);
		}
	}
}