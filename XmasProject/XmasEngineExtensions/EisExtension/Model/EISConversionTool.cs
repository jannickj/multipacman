using JSLibrary.IiLang;
using XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Actions;
using XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Percepts;
using XmasEngineModel.Conversion;

namespace XmasEngineExtensions.EisExtension.Model
{
	public class EisConversionTool : XmasConversionTool<IilElement>
	{

		public EisConversionTool()
		{
			this.AddConverter(new GetAllPerceptsActionConverter());
			this.AddConverter(new EISPerceptCollectionSerializer());
			this.AddConverter(new EISSingleNumeralSerializer());
			
			

		}
	}
}