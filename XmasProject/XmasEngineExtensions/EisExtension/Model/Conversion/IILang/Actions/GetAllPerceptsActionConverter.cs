using GooseEISExtension.Model.ActionTypes;
using GooseEngine.GameManagement.Actions;

namespace GooseEISExtension.Model.Conversion.IILang.Actions
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPercepts, EISGetAllPercepts>
	{
		public override GetAllPercepts BeginConversionToGoose(EISGetAllPercepts fobj)
		{
			return new GetAllPercepts();
		}
	}
}