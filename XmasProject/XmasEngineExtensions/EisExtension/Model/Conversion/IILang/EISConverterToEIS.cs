using JSLibrary.IiLang;
using XmasEngineModel;
using XmasEngineModel.Conversion;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
	public abstract class EISConverterToEIS<GooseType, EISType> : XmasConverterToForeign<GooseType, EISType>
		where EISType : IILElement
		where GooseType : XmasObject
	{
	}
}