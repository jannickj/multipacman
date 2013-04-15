using System;
using iilang;
using GooseEngine.EIS;

namespace GooseEngine.EIS.Conversion.IILang
{
    public class EISTileSerializer : EISConverter<Tile, IILElement>
	{
		#region implemented abstract members of GooseConverter

		public override Tile BeginConversionToGoose (IILElement gobj)
		{
			throw new NotImplementedException ();
		}

		public override IILElement BeginConversionToForeign (Tile t)
		{
			IILParameterList pl = new IILParameterList();
			foreach (Entity ent in t.Entities)
				pl.AddParameter((IILParameter)this.ConvertToForeign(ent));
			
			return pl;
		}

		#endregion
    }
}