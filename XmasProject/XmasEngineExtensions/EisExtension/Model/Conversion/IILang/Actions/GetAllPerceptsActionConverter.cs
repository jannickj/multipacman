using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineModel.GameManagement.Actions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Actions
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPercepts, EISGetAllPercepts>
	{
		public override GetAllPercepts BeginConversionToGoose(EISGetAllPercepts fobj)
		{
			return new GetAllPercepts();
		}
	}
}