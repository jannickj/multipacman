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
		public override IilPercept BeginConversionToForeign(Vision gobj)
		{
			IilPercept percept = new IilPercept("vision");

			foreach (KeyValuePair<Point, Tile> kvp in gobj.VisibleTiles)
			{
				IilFunction fun = new IilFunction("on",
				                                  new IilNumeral(kvp.Key.X),
				                                  new IilNumeral(kvp.Key.Y),
				                                  new IilIdentifier(kvp.Value.ToString())
					);
				percept.addParameter(fun);
			}

			return percept;
		}
	}
}