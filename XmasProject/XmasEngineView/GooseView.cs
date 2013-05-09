using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Interfaces;

namespace GooseEngineView
{
	public abstract class GooseView : IStartable
	{
		public virtual void Initialize()
		{}

		public abstract void Start();
	}
}
