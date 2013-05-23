using System.Collections.Generic;
using JSLibrary.Data;
using JSLibrary.IiLang;
using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineExtensions.EisExtension.Model.Conversion.IiLang;
using XmasEngineExtensions.TileExtension;
using XmasEngineExtensions.TileExtension.Percepts;
using System.Linq;

namespace XmasEngineExtensions.TileEisExtension.Conversion
{
	public class EisTileVisionSerializer : EISPerceptConverter<TileVisionPercept>
	{
		public override IilPercept BeginConversionToForeign(TileVisionPercept gobj)
		{
			IilParameterList pl = new IilParameterList();
			IilPercept percept = new IilPercept("vision", 
				new IilNumeral(gobj.Position.X),
				new IilNumeral(gobj.Position.Y),
				pl
				);

			pl.AddParameter(gobj.Tile.Entities.Select(ent => new IilIdentifier(ent.GetType().Name.ToLower())));
			return percept;
		}
	}
}