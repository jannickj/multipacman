using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel.Perceptions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Percepts
{
	public class EISSingleNumeralSerializer : EISPerceptConverter<SingleNumeralPercept>
	{
		public override IilPercept BeginConversionToForeign(SingleNumeralPercept gobj)
		{
			return new IilPercept(gobj.Name, new IilNumeral(gobj.Value));
		}
	}
}