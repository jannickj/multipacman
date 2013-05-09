using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverterToXmas<XmasType, ForeignType> : XmasConverter<XmasType, ForeignType>
		where XmasType : XmasObject
	{
		public override ForeignType BeginConversionToForeign(XmasType gobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}