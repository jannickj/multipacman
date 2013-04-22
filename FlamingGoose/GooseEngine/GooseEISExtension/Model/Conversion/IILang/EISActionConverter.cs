using GooseEngine.Conversion;
using GooseEngine.GameManagement;

namespace GooseEISExtension.Model.Conversion.IILang
{
	public abstract class EISActionConverter<ActionType, EISActionType> : GooseConverterToGoose<ActionType, EISActionType>
		where ActionType : EntityGameAction
		where EISActionType : EISAction
	{
	}
}