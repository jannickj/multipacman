using GooseEngine;
using GooseEngine.Conversion;
using iilang;

namespace GooseEISExtension.Model.Conversion.IILang
{
	public abstract class EISConverterToEIS<GooseType, EISType> : GooseConverterToForeign<GooseType, EISType>
		where EISType : IILElement
		where GooseType : GooseObject
	{
	}
}