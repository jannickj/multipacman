using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Parameters;
using XmasEngineModel.Percepts;
using ConsoleXmasImplementation.Model.Percepts;

namespace XmasEngineExtensions.EisExtension.Model.Conversion.IiLang.Percepts
{
	public class EISPackageHoldingSerializer : EISPerceptConverter<PackageHoldingPercept>
	{
		public override IilPercept BeginConversionToForeign(PackageHoldingPercept gobj)
		{
			return new IilPercept(gobj.Name, new IilIden
			return new IilPercept(gobj.Name, new IilNumeral(gobj.Value));
		}
	}
}