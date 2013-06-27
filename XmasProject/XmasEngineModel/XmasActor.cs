using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;

namespace XmasEngineModel
{
	public class XmasActor : XmasObject
	{
		private ActionManager actman;
		private EventManager evtman;
		private XmasFactory factory;
		private XmasWorld world;

		/// <summary>
		/// Gets the world of the engine the Xmas actor is currently part of
		/// </summary>
		public virtual XmasWorld World
		{
			get {
				if (world == null)
					throw new PropertyIsNullException("World", this);
				return world; }
			set { world = value; }
		}

		public TWorld WorldAs<TWorld>() where TWorld : XmasWorld
		{
			return (TWorld) World;
		}

		public virtual XmasFactory Factory
		{
			get 
			{
				if (factory == null)
					throw new PropertyIsNullException("Factory", this);

				return factory; 
			}

			set { factory = value; }
		}

		public TFactory FactoryAs<TFactory>() where TFactory : XmasFactory
		{
			return (TFactory)Factory;
		}


		public virtual EventManager EventManager
		{
			get 
			{
				if (evtman == null)
					throw new PropertyIsNullException("EventManager", this);
				return evtman; 
			}
			set { evtman = value; }
		}

		public TEvtman EventManagerAs<TEvtman>() where TEvtman : EventManager
		{
			return (TEvtman)EventManager;
		}

		public virtual ActionManager ActionManager
		{
			get
			{
				if (actman == null)
					throw new PropertyIsNullException("ActionManager", this);
				return actman; 
			}
			set { actman = value; }
		}

		public TActman ActionManagerAs<TActman>() where TActman : ActionManager
		{
			return (TActman)ActionManager;
		}

		public override string ToString()
		{
			return this.GetType().Name;
		}

	}
}