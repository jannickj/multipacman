using XmasEngineModel.Management;

namespace XmasEngineModel
{
	public class XmasActor : XmasObject
	{
		private ActionManager actman;
		private EventManager evtman;
		private XmasFactory factory;
		private XmasWorld world;

		public virtual XmasWorld World
		{
			get { return world; }
			internal protected set { world = value; }
		}

		public TWorld WorldAs<TWorld>() where TWorld : XmasWorld
		{
			return (TWorld) World;
		}

		public virtual XmasFactory Factory
		{
			get { return factory; }

			internal protected set { factory = value; }
		}

		public TFactory FactoryAs<TFactory>() where TFactory : XmasFactory
		{
			return (TFactory)Factory;
		}


		public virtual EventManager EventManager
		{
			get { return evtman; }
			internal protected set { evtman = value; }
		}

		public TEvtman EventManagerAs<TEvtman>() where TEvtman : EventManager
		{
			return (TEvtman)EventManager;
		}

		public virtual ActionManager ActionManager
		{
			get { return actman; }
			internal protected set { actman = value; }
		}

		public TActman ActionManagerAs<TActman>() where TActman : ActionManager
		{
			return (TActman)ActionManager;
		}
	}
}