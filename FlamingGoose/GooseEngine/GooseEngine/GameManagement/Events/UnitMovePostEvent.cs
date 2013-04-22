using JSLibrary.Data;

namespace GooseEngine.GameManagement.Events
{
	public class UnitMovePostEvent : GameEvent
	{
		private Point newpos;

		public UnitMovePostEvent(Point newpos)
		{
			this.newpos = newpos;
		}

		public Point NewPos
		{
			get { return newpos; }
		}
	}
}