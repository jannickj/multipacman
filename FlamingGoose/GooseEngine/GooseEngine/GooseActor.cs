using GooseEngine.GameManagement;

namespace GooseEngine
{
	public class GooseActor : GooseObject
	{
		private ActionManager actman;
		private EventManager evtman;
		private GooseFactory factory;
		private GooseWorld world;

		public GooseWorld World
		{
			get { return world; }
			internal set { world = value; }
		}

		public GooseFactory Factory
		{
			get { return factory; }

			internal set { factory = value; }
		}


		public EventManager EventManager
		{
			get { return evtman; }
			internal set { evtman = value; }
		}

		public ActionManager ActionManager
		{
			get { return actman; }
			internal set { actman = value; }
		}
	}
}