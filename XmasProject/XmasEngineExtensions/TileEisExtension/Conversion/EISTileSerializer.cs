using JSLibrary.IiLang;
using JSLibrary.IiLang.Parameters;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang;
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
				pl.AddParameter(new IilFunction("entity",new IilIdentifier(ent.GetType().Name.ToLower())));

			
			return new IilFunction("contains",pl); 
		}
	}
}