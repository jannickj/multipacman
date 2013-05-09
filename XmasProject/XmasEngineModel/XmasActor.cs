using XmasEngineModel.Management;

namespace XmasEngineModel
{
	public class XmasActor : XmasObject
	{
		private ActionManager actman;
		private EventManager evtman;
		private XmasFactory factory;
		private XmasWorld world;

		public XmasWorld World
		{
			get { return world; }
			set { world = value; }
		}

		public XmasFactory Factory
		{
			get { return factory; }

			set { factory = value; }
		}


		public EventManager EventManager
		{
			get { return evtman; }
			internal set { evtman = value; }
		}

		public ActionManager ActionManager
		{
			get { return actman; }
			set { actman = value; }
		}
	}
}