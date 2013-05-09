using XmasEngineModel.Conversion;
using XmasEngineModel.GameManagement;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang
{
	public abstract class EISActionConverter<ActionType, EISActionType> : GooseConverterToGoose<ActionType, EISActionType>
		where ActionType : EntityGameAction
		where EISActionType : EISAction
	{
	}
}