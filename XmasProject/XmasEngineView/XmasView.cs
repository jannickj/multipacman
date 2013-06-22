using XmasEngineModel;
using XmasEngineModel.Interfaces;
using XmasEngineModel.Management;

namespace XmasEngineView
{
	public abstract class XmasView : XmasActor, IStartable
	{
		private ThreadSafeEventManager evtmanager;

		public ThreadSafeEventManager ThreadSafeEventManager
		{
			get { return evtmanager; }
		}
		

		public XmasView(ThreadSafeEventManager evtmanager)
		{
			this.evtmanager = evtmanager;
		}

		public virtual void Initialize()
		{
		}

		public abstract void Start();
	}
}