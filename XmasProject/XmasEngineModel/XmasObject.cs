namespace XmasEngineModel
{
    /// <summary>
    /// The most basic form of an object used by the engine
    /// </summary>
	public class XmasObject
	{
		private ulong id = 0;

        /// <summary>
        /// The id of the object, this is given to the object by the engine
        /// </summary>
		public ulong Id
		{
			get { return id; }
			internal set { id = value; }
		}
	}
}