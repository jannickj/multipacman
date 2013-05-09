using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineModel.Management.Actions;

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