using System;

namespace GooseEngine
{
	public class EISEntity : Entity, IEISifiable
	{
		public EISEntity () : base()
		{
		}

		#region IEISifiable implementation

		public iilang.IILElement EISify ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

