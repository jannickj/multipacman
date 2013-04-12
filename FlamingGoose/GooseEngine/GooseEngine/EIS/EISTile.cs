using System;
using iilang;

namespace GooseEngine
{
	public class EISTile : Tile, IEISifiable
	{
		#region IEISifiable implementation

		public IILElement EISify ()
		{
			IILParameterList pl = new IILParameterList ();
			foreach (EISEntity ent in Entities)
				pl.AddParameter ((IILParameter) ent.EISify ());

			return pl;
		}

		#endregion
	}
}