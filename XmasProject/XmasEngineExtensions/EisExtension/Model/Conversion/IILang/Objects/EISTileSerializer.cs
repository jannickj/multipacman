using JSLibrary.IiLang;
using JSLibrary.IiLang.Parameters;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Objects
{
	public class EISTileSerializer : EISConverterToEIS<Tile, IilElement>
	{
		public override IilElement BeginConversionToForeign(Tile t)
		{
			IilParameterList pl = new IilParameterList();
			foreach (Entity ent in t.Entities)
				pl.AddParameter((IilParameter) ConvertToForeign(ent));

			return pl;
		}
	}
}