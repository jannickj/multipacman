using JSLibrary.IiLang.DataContainers;
using XmasEngineModel;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
	public abstract class EISPerceptConverter<GooseType> : EISConverterToEIS<GooseType, IILPercept>
		where GooseType : GooseObject
	{
	}
}