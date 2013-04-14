using System;
using iilang;
using GooseEngine.EIS;

namespace GooseEngine
{
	public class EISTileSerializer : EISConverter<Tile>
	{
		#region implemented abstract members of GooseConverter
		public override GooseObject BeginConversionToGoose (Tile t)
		{
			IILParameterList pl = new IILParameterList();
			foreach (Entity ent in t.Entities)
				pl.AddParameter((IILParameter)this.ConvertToForeign(ent));
			
			return pl;
		}
		public override Tile BeginConversionToForeign (GooseObject gobj)
		{
			throw new NotImplementedException ();
		}
		#endregion
    }
}