using JSLibrary.Data;

namespace XmasEngineModel.Management.Events
{
	public class UnitMovePostEvent : XmasEvent
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