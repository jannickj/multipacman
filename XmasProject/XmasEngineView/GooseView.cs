using XmasEngineModel.Interfaces;

namespace XmasEngineView
{
	public abstract class GooseView : IStartable
	{
		public virtual void Initialize()
		{}

		public abstract void Start();
	}
}
