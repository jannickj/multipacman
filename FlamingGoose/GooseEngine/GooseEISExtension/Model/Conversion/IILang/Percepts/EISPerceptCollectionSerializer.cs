using System.Linq;
using GooseEngine;
using iilang;
using iilang.DataContainers;

namespace GooseEISExtension.Model.Conversion.IILang.Percepts
{
	public class EISPerceptCollectionSerializer : EISConverterToEIS<PerceptCollection, IILPerceptCollection>
	{
		public override IILPerceptCollection BeginConversionToForeign(PerceptCollection gobj)
		{
			return new IILPerceptCollection(gobj.Percepts.Select(p => (IILPercept) ConvertToForeign(p)).ToArray());
		}
	}
}