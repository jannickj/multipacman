using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineModel.Management.Actions;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Actions
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPercepts, EISGetAllPercepts>
	{
		public override GetAllPercepts BeginConversionToXmas(EISGetAllPercepts fobj)
		{
			return new GetAllPercepts();
		}
	}
}