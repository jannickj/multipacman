using XmasEngineModel;
using XmasEngineModel.Interfaces;

namespace XmasEngineView
{
	public abstract class XmasView : XmasActor, IStartable
	{
		public virtual void Initialize()
		{
		}

		public abstract void Start();
	}
}