using System.Linq;
using JSLibrary.IiLang;
using JSLibrary.IiLang.DataContainers;
using XmasEngineModel;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IILang.Percepts
{
	public class EISPerceptCollectionSerializer : EISConverterToEIS<PerceptCollection, IILPerceptCollection>
	{
		public override IILPerceptCollection BeginConversionToForeign(PerceptCollection gobj)
		{
			return new IILPerceptCollection(gobj.Percepts.Select(p => (IILPercept) ConvertToForeign(p)).ToArray());
		}
	}
}