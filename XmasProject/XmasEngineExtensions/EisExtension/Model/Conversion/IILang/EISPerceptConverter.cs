using GooseEngine;
using iilang.DataContainers;

namespace GooseEISExtension.Model.Conversion.IILang
{
	public abstract class EISPerceptConverter<GooseType> : EISConverterToEIS<GooseType, IILPercept>
		where GooseType : GooseObject
	{
	}
}