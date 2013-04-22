using GooseEngine.Perceptions;
using iilang.DataContainers;
using iilang.Parameters;

namespace GooseEISExtension.Model.Conversion.IILang.Percepts
{
	public class EISSingleNumeralSerializer : EISPerceptConverter<SingleNumeralPercept>
	{
		public override IILPercept BeginConversionToForeign(SingleNumeralPercept gobj)
		{
			return new IILPercept(gobj.Name, new IILNumeral(gobj.Value));
		}
	}
}