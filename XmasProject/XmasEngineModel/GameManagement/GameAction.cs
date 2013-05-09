using System;

namespace GooseEngine.GameManagement
{
	public abstract class GameAction : GooseActor
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