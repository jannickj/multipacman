using XmasEngineModel.Conversion;
using XmasEngineModel.Management;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
	public abstract class EISActionConverter<ActionType, EISActionType> : XmasConverterToXmas<ActionType, EISActionType>
		where ActionType : EntityXmasAction
		where EISActionType : EISAction
	{
	}
}