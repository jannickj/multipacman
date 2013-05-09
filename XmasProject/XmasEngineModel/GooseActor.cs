using XmasEngineModel.GameManagement;

namespace XmasEngineModel
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
			set { world = value; }
		}

		public GooseFactory Factory
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