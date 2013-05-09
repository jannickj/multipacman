using JSLibrary.IiLang;
using XmasEngineModel;
using XmasEngineModel.Conversion;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
	public abstract class EISConverterToEIS<GooseType, EISType> : GooseConverterToForeign<GooseType, EISType>
		where EISType : IILElement
		where GooseType : GooseObject
	{
	}
}