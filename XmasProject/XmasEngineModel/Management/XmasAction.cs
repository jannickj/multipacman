using System;

namespace XmasEngineModel.Management
{
	public abstract class XmasAction : XmasActor
	{
		public event EventHandler Completed;

		internal void Fire()
		{
			Execute();
		}

		protected abstract void Execute();

		protected void Complete()
		{
			if (Completed != null)
				Completed(this, new EventArgs());
		}
	}
}