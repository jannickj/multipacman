using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel.Perceptions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Percepts
{
	public class EISSingleNumeralSerializer : EISPerceptConverter<SingleNumeralPercept>
	{
		public override IILPercept BeginConversionToForeign(SingleNumeralPercept gobj)
		{
			return new IILPercept(gobj.Name, new IILNumeral(gobj.Value));
		}
	}
}