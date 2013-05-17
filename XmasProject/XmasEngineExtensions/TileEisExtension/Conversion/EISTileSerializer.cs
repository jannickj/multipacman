using JSLibrary.IiLang;
using JSLibrary.IiLang.Parameters;
using XmasEngineExtensions.EisExtension.Model.Conversion.IILang;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.EntityLib;

namespace XmasEngineExtensions.TileEisExtension.Conversion
{
	public class EISTileSerializer : EISConverterToEIS<Tile, IilElement>
	{
		public override IilElement BeginConversionToForeign(Tile t)
		{
			IilParameterList pl = new IilParameterList();
			foreach (XmasEntity ent in t.Entities)
				pl.AddParameter((IilParameter) ConvertToForeign(ent));

			return pl;
		}
	}
}