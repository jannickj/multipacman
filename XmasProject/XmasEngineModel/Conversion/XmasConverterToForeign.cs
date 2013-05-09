using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverterToForeign<GooseType, ForeignType> : XmasConverter<GooseType, ForeignType>
		where GooseType : XmasObject
	{
		public override GooseType BeginConversionToGoose(ForeignType fobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}