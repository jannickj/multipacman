using GooseEngine;
using iilang;
using iilang.Parameters;

namespace GooseEISExtension.Model.Conversion.IILang.Objects
{
	public class EISTileSerializer : EISConverterToEIS<Tile, IILElement>
	{
		public override IILElement BeginConversionToForeign(Tile t)
		{
			IILParameterList pl = new IILParameterList();
			foreach (Entity ent in t.Entities)
				pl.AddParameter((IILParameter) ConvertToForeign(ent));

			return pl;
		}
	}
}