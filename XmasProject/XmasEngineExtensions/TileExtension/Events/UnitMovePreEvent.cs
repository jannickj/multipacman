using XmasEngineModel.Management;

namespace XmasEngineExtensions.TileExtension.Events
{
	public class UnitMovePreEvent : XmasEvent
	{
		public bool IsStopped { get; set; }
	}
}