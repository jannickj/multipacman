using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverterToForeign<XmasType, ForeignType> : XmasConverter<XmasType, ForeignType>
		where XmasType : XmasObject
	{
		public override XmasType BeginConversionToXmas(ForeignType fobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}