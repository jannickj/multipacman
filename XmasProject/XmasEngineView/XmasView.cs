using XmasEngineModel.Interfaces;

namespace XmasEngineView
{
	public abstract class XmasView : IStartable
	{
		public virtual void Initialize()
		{
			
		}

		public abstract void Start();
	}
}
