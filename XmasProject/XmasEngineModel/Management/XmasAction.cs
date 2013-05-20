using System;

namespace XmasEngineModel.Management
{
	public abstract class XmasAction : XmasActor
	{
		public event EventHandler Completed;
		public event EventHandler Failed;
		public event EventHandler Resolved;
		private bool actionfailed = false;

		public bool ActionFailed
		{
			get { return actionfailed; }
		}

		internal void Fire()
		{
			Execute();
		}

		protected abstract void Execute();

		protected void Complete()
		{
			var buffer = Completed;
			if (buffer != null)
				buffer(this, new EventArgs());

			Resolve();

		}

		internal void Fail()
		{
			this.actionfailed = true;
			var buffer = Failed;
			if (buffer != null)
				buffer(this, new EventArgs());
			Resolve();

		}

		private void Resolve()
		{
			var buffer = Resolved;
			if (buffer != null)
				buffer(this, new EventArgs());
		}
	}
}