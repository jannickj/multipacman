using XmasEngineExtensions.EisExtension.Model.ActionTypes;
using XmasEngineExtensions.EisExtension.Model.Conversion.IILang;
using XmasEngineModel.Management.Actions;

namespace XmasEngineExtensions.EisTileExtension
{
	public class GetAllPerceptsActionConverter : EISActionConverter<GetAllPerceptsAction, EISGetAllPercepts>
	{
		public override GetAllPerceptsAction BeginConversionToXmas(EISGetAllPercepts fobj)
		{
			return new GetAllPerceptsAction();
		}
	}
}