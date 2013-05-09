using JSLibrary.IiLang;
using XmasEngineModel;
using XmasEngineModel.Conversion;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
<<<<<<< Upstream, based on origin/master
	public abstract class EISConverterToEIS<GooseType, EISType> : XmasConverterToForeign<GooseType, EISType>
		where EISType : IILElement
		where GooseType : XmasObject
=======
	public abstract class EISConverterToEIS<GooseType, EISType> : GooseConverterToForeign<GooseType, EISType>
		where EISType : IilElement
		where GooseType : GooseObject
>>>>>>> 5eb9fe6 C#: refactored Goose -> Xmas
	{
	}
}
