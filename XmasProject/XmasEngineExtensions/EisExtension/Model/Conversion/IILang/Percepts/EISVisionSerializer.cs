using System.Collections.Generic;
using JSLibrary.Data;
using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel;
using XmasEngineModel.Perceptions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Percepts
{
	public class EISVisionSerializer : EISPerceptConverter<Vision>
	{
		public override IILPercept BeginConversionToForeign(Vision gobj)
		{
			IILPercept percept = new IILPercept("vision");

			foreach (KeyValuePair<Point, Tile> kvp in gobj.VisibleTiles)
			{
				IILFunction fun = new IILFunction("on",
				                                  new IILNumeral(kvp.Key.X),
				                                  new IILNumeral(kvp.Key.Y),
				                                  new IILIdentifier(kvp.Value.ToString())
					);
				percept.addParameter(fun);
			}

			return percept;
		}
	}
}