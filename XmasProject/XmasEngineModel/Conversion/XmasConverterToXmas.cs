using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverterToXmas<GooseType, ForeignType> : XmasConverter<GooseType, ForeignType>
		where GooseType : XmasObject
	{
		public override ForeignType BeginConversionToForeign(GooseType gobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}