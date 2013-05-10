using JSLibrary.IiLang.DataContainers;
using XmasEngineModel;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
	public abstract class EISPerceptConverter<XmasType> : EISConverterToEIS<XmasType, IilPercept>
		where XmasType : XmasObject
	{
	}
}