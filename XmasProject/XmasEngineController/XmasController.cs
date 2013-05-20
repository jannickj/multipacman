using System.Collections.Generic;
using System.Threading;
using XmasEngineController.AI;
using XmasEngineModel;
using XmasEngineView;

namespace XmasEngineController
{
	public abstract class XmasController : XmasActor
	{

		
		public virtual void Initialize()
		{

		}

		public virtual void Start()
		{
			
		}

		public virtual string ThreadName()
		{
			return "Controller Thread";
		}
	}
}