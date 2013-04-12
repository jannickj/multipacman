using System;
using GooseEngine.GameManagement;

namespace GooseEngine
{
	public class GooseActor : GooseObject
	{
		private GameWorld world;
		private GameFactory factory;
		private ActionManager actman;
		private EventManager evtman;

		public GooseActor ()
		{
		}

		public GameWorld World {
			get {
				return world;
			}
			internal set {
				this.world = value;
			}
		}
		
		public GameFactory Factory {
			get {
				return factory;
			}
			
			internal set {
				this.factory = value;
			}
		}
		
		
		public EventManager EventManager {
			get { 
				return evtman; 
			}
			internal set { 
				evtman = value; 
			}
		}
		
		public ActionManager ActionManager {
			get {
				return actman;
			}
			internal set {
				actman = value;
			}
		}
	}
}

