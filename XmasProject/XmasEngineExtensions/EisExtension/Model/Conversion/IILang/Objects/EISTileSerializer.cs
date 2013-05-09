using JSLibrary.IiLang;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Objects
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