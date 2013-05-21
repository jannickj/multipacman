namespace XmasEngineModel
{
	public class XmasObject
	{
		private ulong id = 0;

		public ulong Id
		{
			get { return id; }
			internal set { id = value; }
		}
	}
}