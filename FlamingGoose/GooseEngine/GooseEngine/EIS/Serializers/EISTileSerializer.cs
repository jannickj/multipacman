using System;
using iilang;

namespace GooseEngine
{
	public class EISTileSerializer : EISSerializer<Tile>
	{
		#region IEISifiable implementation

		public IILElement EISify (Tile t)
		{
			IILParameterList pl = new IILParameterList ();
			foreach (Entity ent in t.Entities)
				pl.AddParameter ((IILParameter) ent.ToSerializableObject ());

			return pl;
		}

		#endregion
	}
}